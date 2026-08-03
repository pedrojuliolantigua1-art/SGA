
namespace SGA.Desktop.DTOs.Acceso
{
    public sealed class AccesoPresentacionDto
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public int ViajeId { get; set; }
        public int? AutorizacionTransporteId { get; set; }
        public int ResultadoAcceso { get; set; }
        public string? MotivoRechazo { get; set; }
        public DateTime FechaHora { get; set; }
        public int ValidadoPorUsuarioId { get; set; }
    }
}