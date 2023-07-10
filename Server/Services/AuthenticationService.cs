using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Configuration;
using Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net;

namespace Server.Services
{
    public class AuthenticationService
    {
        private readonly AppSettings _appSettings;

        public AuthenticationService(IOptionsSnapshot<AppSettings> configuration)
        {
            _appSettings = configuration.Value;
        }

        private string GenerateToken(string email, string nombre, string apellido, string id)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("Id", id),
                    new Claim("Nombre", nombre),
                    new Claim("Apellido", apellido)
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_appSettings.TokenAuthentication.Key));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _appSettings.TokenAuthentication.Issuer,
                    audience: _appSettings.TokenAuthentication.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_appSettings.TokenAuthentication.ExpirationInMinutes),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateToken(User user)
        {
            return GenerateToken(user.Email ?? string.Empty, user.FirstName ?? string.Empty, user.LastName ?? string.Empty, user.Id.ToString());

        }
    }
}
