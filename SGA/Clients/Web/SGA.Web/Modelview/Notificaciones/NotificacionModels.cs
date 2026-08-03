namespace SGA.Web.Models.Notificaciones
{
    public sealed class NotificacionModel
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public bool Leida { get; set; }
    }
}
