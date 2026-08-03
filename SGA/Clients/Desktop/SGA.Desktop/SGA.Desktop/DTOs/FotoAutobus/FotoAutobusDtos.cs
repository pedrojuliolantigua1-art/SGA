namespace SGA.Desktop.DTOs.FotoAutobus
{
    public sealed class FotoAutobusPresentacionDto
    {
        public int Id { get; set; }
        public int AutobusId { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public string UrlPublica { get; set; } = string.Empty;
        public DateTime FechaSubida { get; set; }
    }
}
