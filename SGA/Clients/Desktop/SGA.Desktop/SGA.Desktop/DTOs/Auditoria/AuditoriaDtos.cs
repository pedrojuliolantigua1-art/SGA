namespace SGA.Desktop.DTOs.Auditoria
{
    public sealed class AuditoriaPresentacionDto
    {
        public int Id { get; set; }
        public int UsuarioTransporteId { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? Accion { get; set; }
        public string? EntidadAfectada { get; set; }
        public string? EntidadId { get; set; }
        public string? Detalle { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
