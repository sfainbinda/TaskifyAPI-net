using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Server.Configuration;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;


var builder = WebApplication.CreateBuilder(args);

// Obtener la configuración
/* appsettings.json como fuente de la configuración. 
* optional: El archivo es obligatorio y lanza una excepción si no lo encuentra. 
* reload on change: La configuración se recarga automáticamente si el archivo cambia durante la ejecución de la aplicación. */
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Agregar la configuración de AppSettings
builder.Services.Configure<AppSettings>(configuration);

// Obtener la instancia AppSettings
var appSettings = configuration.Get<AppSettings>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = appSettings?.ConnectionStrings!.Default;
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Agrega el servicio de autenticación JWT.
builder.Services.AddAuthentication(options =>
{
    // Configura el esquema de autenticación predeterminado como JWT bearer authentication.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // Configura la validación del token JWT.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = appSettings?.TokenAuthentication!.Issuer, // "Issuer" en el token debe coincidir con este valor.
        ValidAudience = appSettings?.TokenAuthentication!.Audience, // "Audience" en el token debe coincidir con este valor.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings?.TokenAuthentication?.Key!)) // "Key" valida la autenticidad del token.
    };

    // Opción extra para usar el token desde la cookie.
    options.Events = new JwtBearerEvents
    {
        // Más información en: https://medium.com/@alm.ozdmr/asp-net-core-jwt-and-refresh-token-with-httponly-cookies-b1b96c849742
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey(appSettings?.TokenAuthentication?.CookieToken!))
            {
                context.Token = context.Request.Cookies[appSettings?.TokenAuthentication?.CookieToken!];
            }
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();