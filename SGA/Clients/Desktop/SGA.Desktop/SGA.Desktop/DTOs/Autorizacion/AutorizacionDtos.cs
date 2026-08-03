namespace SGA.Desktop.DTOs.Autorizacion
{
    public sealed class AutorizacionResumenPresentacionDto
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public string? TipoAutorizacion { get; set; }
        public DateTime FechaEmision { get; set; }

        // Llega como número desde la API (EstadoAutorizacion sin JsonStringEnumConverter).
        public int Estado { get; set; }
    }

    public sealed class CrearTicketDiarioPresentacionDto
    {
        public int UsuarioTransporteId { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.Today;
        public DateTime FechaFin { get; set; } = DateTime.Today.AddMonths(1);
        public string? CreadoPor { get; set; }
    }

    public sealed class TicketDiarioPresentacionDto
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public DateTime FechaEmision { get; set; }
        public int Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public sealed class CrearTarjetaRecargablePresentacionDto
    {
        public int UsuarioTransporteId { get; set; }
        public decimal SaldoInicial { get; set; }
        public string? NumeroTarjeta { get; set; }
        public string? CreadoPor { get; set; }
    }

    public sealed class TarjetaRecargablePresentacionDto
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public DateTime FechaEmision { get; set; }
        public int Estado { get; set; }
        public string? NumeroTarjeta { get; set; }
        public decimal SaldoDisponible { get; set; }
    }

    public sealed class CrearPermisoPresentacionDto
    {
        public int UsuarioTransporteId { get; set; }
        public string? CondicionInstitucional { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string? CreadoPor { get; set; }
    }

    public sealed class PermisoPresentacionDto
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public DateTime FechaEmision { get; set; }
        public int Estado { get; set; }
        public string? CondicionInstitucional { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }

    public sealed class AnularAutorizacionPresentacionDto
    {
        public int Id { get; set; }
        public string? Motivo { get; set; }
        public string? AnuladoPor { get; set; }
    }

    public sealed class RecargarBilleteraPresentacionDto
    {
        public int UsuarioTransporteId { get; set; }
        public decimal Monto { get; set; }
        public int RegistradoPorUsuarioId { get; set; }
        public string? CreadoPor { get; set; }
    }

    public sealed class BilleteraPresentacionDto
    {
        public int AutorizacionId { get; set; }
        public string? NumeroTarjeta { get; set; }
        public decimal SaldoDisponible { get; set; }
        public bool FueCreada { get; set; }
    }
}
