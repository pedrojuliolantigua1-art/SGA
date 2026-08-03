namespace SGA.Desktop.DTOs.Notificacion
{
    public sealed class NotificacionPresentacionDto
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public bool Leida { get; set; }
    }

    public sealed class CrearNotificacionPresentacionDto
    {
        public int UsuarioTransporteId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string? CreadoPor { get; set; }
    }
}