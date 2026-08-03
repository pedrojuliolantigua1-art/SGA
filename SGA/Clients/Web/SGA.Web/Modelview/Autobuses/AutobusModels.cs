namespace SGA.Web.Models.Autobuses
{
    public sealed class AutobusModel
    {
        public int Id { get; set; }
        public string? Placa { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Capacidad { get; set; }
        public string Estado { get; set; } = "Disponible";
    }

    public sealed class CrearAutobusModel
    {
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Capacidad { get; set; } = 1;
        public string? CreadoPor { get; set; }
    }

    public sealed class FotoAutobusModel
    {
        public int Id { get; set; }
        public int AutobusId { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public string UrlPublica { get; set; } = string.Empty;
        public DateTime FechaSubida { get; set; }
    }
}
