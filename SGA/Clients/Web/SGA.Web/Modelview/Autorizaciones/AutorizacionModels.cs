namespace SGA.Web.Models.Autorizaciones
{
    public sealed class AutorizacionResumenModel
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public string? TipoAutorizacion { get; set; }
        public DateTime FechaEmision { get; set; }

        // Llega como número desde la API (EstadoAutorizacion sin JsonStringEnumConverter).
        public int Estado { get; set; }

        public string DescripcionEstado => Estado switch
        {
            1 => "Activa",
            2 => "Vencida",
            3 => "Consumida",
            4 => "Inactiva",
            5 => "Anulada",
            _ => "Desconocido"
        };
    }

    /// <summary>Refleja TarjetaRecargableDto de la API — se usa cuando TipoAutorizacion es "TarjetaRecargable".</summary>
    public sealed class TarjetaRecargableModel
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public DateTime FechaEmision { get; set; }
        public int Estado { get; set; }
        public string? NumeroTarjeta { get; set; }
        public decimal SaldoDisponible { get; set; }

        public string DescripcionEstado => Estado switch
        {
            1 => "Activa",
            2 => "Vencida",
            3 => "Consumida",
            4 => "Inactiva",
            5 => "Anulada",
            _ => "Desconocido"
        };

        public string ClaseEstado => Estado switch
        {
            1 => "estado-completado",
            3 or 4 or 5 => "estado-cancelado",
            2 => "estado-retrasado",
            _ => ""
        };
    }
}
