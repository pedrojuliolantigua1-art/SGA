namespace SGA.Web.Models.Viajes
{
    public sealed class ViajeModel
    {
        public int Id { get; set; }
        public int RutaId { get; set; }
        public int HorarioRutaId { get; set; }
        public int AutobusId { get; set; }
        public int ConductorId { get; set; }
        public DateTime Fecha { get; set; }

        // Llega como número desde la API (EstadoViaje sin JsonStringEnumConverter).
        public int Estado { get; set; }
        public DateTime? HoraInicioReal { get; set; }
        public DateTime? HoraFinReal { get; set; }
        public int CupoActual { get; set; }
        public int CapacidadMaxima { get; set; }

        // Datos auxiliares que la UI completa localmente (no vienen de /api/viajes).
        public string? NombreRuta { get; set; }

        public string DescripcionEstado => Estado switch
        {
            1 => "Programado",
            2 => "En Curso",
            3 => "Completado",
            4 => "Cancelado",
            5 => "Retrasado",
            _ => "Desconocido"
        };

        public string ClaseEstado => Estado switch
        {
            1 => "estado-programado",
            2 => "estado-en-curso",
            3 => "estado-completado",
            4 => "estado-cancelado",
            5 => "estado-retrasado",
            _ => ""
        };
    }

    public sealed class EjecutarViajeModel
    {
        public int ViajeId { get; set; }
        public int ConductorId { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;
    }

    public sealed class IncidenciaModel
    {
        public int Id { get; set; }
        public int ViajeId { get; set; }
        public int ConductorId { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaHora { get; set; }
        public string? ConductorNombre { get; set; }
    }

    public sealed class ReportarIncidenciaModel
    {
        public int ViajeId { get; set; }
        public int ConductorId { get; set; }
        public string Tipo { get; set; } = "Retraso";
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string? CreadoPor { get; set; }
        public bool EsAdmin { get; set; }
    }
}
