namespace SGA.Desktop.DTOs.Common
{
    /// <summary>DTO común para operaciones de baja/eliminación en cualquier módulo (motivo + responsable).</summary>
    public sealed class EliminarPresentacionDto
    {
        public string? Motivo { get; set; }
        public string? EliminadoPor { get; set; }
    }
}
