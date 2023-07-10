using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Server.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Obtener la configuración
var configuration = new ConfigurationBuilder() // La clase proporciona métodos para construir la configuración de la aplicación desde distintas fuentes.
    .SetBasePath(builder.Environment.ContentRootPath) // Se establece la ruta base de la configuración en el directorio raíz del proyecto. 
    /* appsettings.json como fuente de la configuración. 
     * optional: El archivo es obligatorio y lanza una excepción si no lo encuentra. 
     * reload on change: La configuración se recarga automáticamente si el archivo cambia durante la ejecución de la aplicación. */
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

//var connectionString = configuration.GetConnectionString("Default");
var connectionString = appSettings.ConnectionStrings.Default;
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
        // El emisor del token debe coincidir exactamente con este valor para que la validación sea exitosa.
        ValidIssuer = appSettings.TokenAuthentication.Issuer,
        // La audiencia del token debe coincidir exactamente con este valor para que la validación sea exitosa.
        ValidAudience = appSettings.TokenAuthentication.Audience,
        /* Especifica la clave de firma utilizada para verificar la autenticidad del token. 
         * Aquí se está utilizando una clave secreta almacenada en la configuración de la aplicación.*/
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.TokenAuthentication.Key))
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
