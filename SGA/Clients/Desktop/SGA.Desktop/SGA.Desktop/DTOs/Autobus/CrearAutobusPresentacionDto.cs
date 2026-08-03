
namespace SGA.Desktop.DTOs.Autobus
{
    public sealed class CrearAutobusPresentacionDto
    {
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public string? CreadoPor { get; set; }
    }
}
