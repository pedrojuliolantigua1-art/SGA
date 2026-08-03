namespace SGA.Desktop.DTOs.Horario
{
    public sealed class HorarioRutaPresentacionDto
    {
        public int Id { get; set; }
        public int RutaId { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegadaEstimada { get; set; }
        public bool Activo { get; set; }
    }

    public sealed class CrearHorarioRutaPresentacionDto
    {
        public int RutaId { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegadaEstimada { get; set; }
        public string? CreadoPor { get; set; }
    }
}
