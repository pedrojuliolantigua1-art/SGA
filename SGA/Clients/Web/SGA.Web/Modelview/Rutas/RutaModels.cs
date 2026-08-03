namespace SGA.Web.Models.Rutas
{
    public sealed class RutaModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Activa { get; set; }
    }

    public sealed class ParadaModel
    {
        public int Id { get; set; }
        public int RutaId { get; set; }
        public string? Nombre { get; set; }
        public string? Referencia { get; set; }
        public int Orden { get; set; }
    }

    public sealed class HorarioRutaModel
    {
        public int Id { get; set; }
        public int RutaId { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegadaEstimada { get; set; }
        public bool Activo { get; set; }
    }

    public sealed class RutaDetalleModel
    {
        public RutaModel Ruta { get; set; } = new();
        public List<ParadaModel> Paradas { get; set; } = new();
        public List<HorarioRutaModel> Horarios { get; set; } = new();
    }
}
