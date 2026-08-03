
namespace SGA.Desktop.DTOs.Autobus
{
    public sealed class AutobusPresentacionDto
    {
        public int Id { get; set; }
        public string? Placa { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Capacidad { get; set; }
        public string Estado { get; set; } = string.Empty;
    }

}
