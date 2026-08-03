namespace SGA.Web.Models.Accesos
{
    public sealed class AccesoModel
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public int ViajeId { get; set; }
        public int? AutorizacionTransporteId { get; set; }

        // Llega como número desde la API (ResultadoAcceso sin JsonStringEnumConverter).
        public int ResultadoAcceso { get; set; }
        public string? MotivoRechazo { get; set; }
        public DateTime FechaHora { get; set; }
        public int ValidadoPorUsuarioId { get; set; }

        public bool FuePermitido => ResultadoAcceso == 1;

        /// <summary>
        /// Código corto derivado del Id para que el estudiante lo muestre y el conductor lo escriba.
        /// No es un campo propio de la API todavía — si más adelante se agrega un campo real
        /// de código en el backend, reemplazar esto por ese valor.
        /// </summary>
        public string CodigoTicket => $"TCK-{Id:D6}";

        public string DescripcionResultado => ResultadoAcceso switch
        {
            1 => "Permitido",
            2 => "Denegado",
            3 => "Autorización vencida",
            4 => "Sin cupo",
            5 => "Usuario inactivo",
            6 => "Saldo insuficiente",
            7 => "Sin autorización",
            8 => "Viaje no disponible",
            9 => "Autorización inválida",
            _ => "Desconocido"
        };
    }

    public sealed class RegistrarAccesoModel
    {
        public int UsuarioTransporteId { get; set; }
        public int ViajeId { get; set; }
        public int ValidadoPorUsuarioId { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public decimal CostoViaje { get; set; }
        public string? CreadoPor { get; set; }
    }
}
