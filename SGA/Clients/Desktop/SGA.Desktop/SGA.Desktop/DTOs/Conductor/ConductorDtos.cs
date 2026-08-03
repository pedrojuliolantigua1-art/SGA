namespace SGA.Desktop.DTOs.Conductor
{
    public sealed class ConductorPresentacionDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? NumeroLicencia { get; set; }
        public DateTime? FechaVencimientoLicencia { get; set; }
        public bool Disponible { get; set; }
    }

    public sealed class CrearConductorPresentacionDto
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? PasswordHash { get; set; }
        public string? NumeroLicencia { get; set; }
        public DateTime? FechaVencimientoLicencia { get; set; }
        public string? CreadoPor { get; set; }
    }

    public sealed class ActualizarConductorPresentacionDto
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? NumeroLicencia { get; set; }
        public DateTime? FechaVencimientoLicencia { get; set; }
    }

    public sealed class CambiarDisponibilidadPresentacionDto
    {
        public bool Disponible { get; set; }
    }
}
