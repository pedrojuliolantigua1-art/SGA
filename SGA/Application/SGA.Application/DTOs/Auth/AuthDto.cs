using SGA.Domain.Enum;

namespace SGA.Application.DTOs.Auth
{
    public sealed record LoginDto(string Correo, string Password);

    public sealed record SesionDto(
        int Id, string? Nombre, string? Apellido, string? Correo,
        RolUsuario RolSistema, string TipoUsuario);

    /// <summary>
    /// Respuesta del endpoint /api/auth/login.
    /// Incluye el token JWT y los datos de sesión del usuario autenticado.
    /// </summary>
    public sealed record LoginResponseDto(
        string Token,
        string TipoToken,
        SesionDto Usuario);
}
