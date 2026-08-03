namespace SGA.Desktop.DTOs.Pago
{
    public sealed class PagoPresentacionDto
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public int AutorizacionTransporteId { get; set; }
        public decimal Monto { get; set; }
        public string? TipoPago { get; set; }

        // Llega como número desde la API (EstadoPago sin JsonStringEnumConverter).
        public int Estado { get; set; }
        public DateTime FechaHora { get; set; }
        public int RegistradoPorUsuarioId { get; set; }
    }

    public sealed class RegistrarPagoPresentacionDto
    {
        public int UsuarioTransporteId { get; set; }
        public decimal Monto { get; set; }
        public string TipoPago { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public int RegistradoPorUsuarioId { get; set; }
        public string? CreadoPor { get; set; }
    }
}
