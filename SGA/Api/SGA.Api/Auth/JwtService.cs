using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SGA.Application.DTOs.Auth;
using SGA.Application.Interfaces.Services;

namespace SGA.Api.Auth
{
    /// <summary>
    /// Genera tokens JWT firmados con HS256.
    /// Configuración en appsettings.json → sección "Jwt".
    /// </summary>
    public sealed class JwtService : IJwtService
    {
        private readonly string _llave;
        private readonly string _emisor;
        private readonly string _audiencia;
        private readonly int _expiracionHoras;

        public JwtService(IConfiguration config)
        {
            _llave = config["Jwt:Llave"]
                ?? throw new InvalidOperationException("Falta la configuración 'Jwt:Llave' en appsettings.json");
            _emisor = config["Jwt:Emisor"] ?? "SGA.Api";
            _audiencia = config["Jwt:Audiencia"] ?? "SGA.Clientes";
            _expiracionHoras = int.TryParse(config["Jwt:ExpiracionHoras"], out var h) ? h : 8;
        }

        public string GenerarToken(SesionDto sesion)
        {
            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_llave));
            var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,   sesion.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, sesion.Correo ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.GivenName, $"{sesion.Nombre} {sesion.Apellido}".Trim()),
                new Claim("rol",       sesion.RolSistema.ToString()),
                new Claim("tipo",      sesion.TipoUsuario),
                new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer:             _emisor,
                audience:           _audiencia,
                claims:             claims,
                expires:            DateTime.UtcNow.AddHours(_expiracionHoras),
                signingCredentials: credenciales);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
