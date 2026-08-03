using SGA.Application.DTOs.Auth;

namespace SGA.Application.Interfaces.Services
{
    /// <summary>
    /// Genera un token JWT a partir de una sesión autenticada.
    /// La interfaz vive en Application; la implementación vive en la API (no viola la dirección de dependencias).
    /// </summary>
    public interface IJwtService
    {
        string GenerarToken(SesionDto sesion);
    }
}
