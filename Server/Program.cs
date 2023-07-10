using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Server.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Obtener la configuraci�n
var configuration = new ConfigurationBuilder() // La clase proporciona m�todos para construir la configuraci�n de la aplicaci�n desde distintas fuentes.
    .SetBasePath(builder.Environment.ContentRootPath) // Se establece la ruta base de la configuraci�n en el directorio ra�z del proyecto. 
    /* appsettings.json como fuente de la configuraci�n. 
     * optional: El archivo es obligatorio y lanza una excepci�n si no lo encuentra. 
     * reload on change: La configuraci�n se recarga autom�ticamente si el archivo cambia durante la ejecuci�n de la aplicaci�n. */
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
    .Build();

// Agregar la configuraci�n de AppSettings
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


// Agrega el servicio de autenticaci�n JWT.
builder.Services.AddAuthentication(options =>
{
    // Configura el esquema de autenticaci�n predeterminado como JWT bearer authentication.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // Configura la validaci�n del token JWT.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        // El emisor del token debe coincidir exactamente con este valor para que la validaci�n sea exitosa.
        ValidIssuer = appSettings.TokenAuthentication.Issuer,
        // La audiencia del token debe coincidir exactamente con este valor para que la validaci�n sea exitosa.
        ValidAudience = appSettings.TokenAuthentication.Audience,
        /* Especifica la clave de firma utilizada para verificar la autenticidad del token. 
         * Aqu� se est� utilizando una clave secreta almacenada en la configuraci�n de la aplicaci�n.*/
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
