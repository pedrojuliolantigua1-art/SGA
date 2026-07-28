using SGA.Domain.Enum;

namespace SGA.Application.DTOs.Auth
{
    public sealed record LoginDto(string Correo, string Password);

    public sealed record SesionDto(
        int Id, string? Nombre, string? Apellido, string? Correo,
        RolUsuario RolSistema, string TipoUsuario);
}
