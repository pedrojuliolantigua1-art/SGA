namespace SGA.Desktop.DTOs.Viaje
{
    public sealed class ViajePresentacionDto
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

        public static string DescribirEstado(int estado) => estado switch
        {
            1 => "Programado",
            2 => "En Curso",
            3 => "Completado",
            4 => "Cancelado",
            5 => "Retrasado",
            _ => "Desconocido"
        };
    }

    public sealed class ProgramarViajePresentacionDto
    {
        public int RutaId { get; set; }
        public int HorarioRutaId { get; set; }
        public int AutobusId { get; set; }
        public int ConductorId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string? CreadoPor { get; set; }
    }

    public sealed class CancelarViajePresentacionDto
    {
        public int ViajeId { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string? CreadoPor { get; set; }
    }

    public sealed class ProgramarSemanaPresentacionDto
    {
        public int RutaId { get; set; }
        public int HorarioRutaId { get; set; }
        public int AutobusId { get; set; }
        public int ConductorId { get; set; }
        public DateTime FechaReferenciaSemana { get; set; } = DateTime.Today;

        /// <summary>Valores de System.DayOfWeek (Domingo=0 ... Sábado=6).</summary>
        public List<int> Dias { get; set; } = new();
        public string? CreadoPor { get; set; }
    }

    public sealed class ProgramarSemanaResultadoPresentacionDto
    {
        public List<ViajePresentacionDto> Creados { get; set; } = new();
        public List<string> Errores { get; set; } = new();
    }

    public sealed class IncidenciaPresentacionDto
    {
        public int Id { get; set; }
        public int ViajeId { get; set; }
        public int ConductorId { get; set; }
        public string? ConductorNombre { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaHora { get; set; }
    }

    public sealed class ReportarIncidenciaPresentacionDto
    {
        public int ViajeId { get; set; }
        public int ConductorId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string? CreadoPor { get; set; }
    }
}
