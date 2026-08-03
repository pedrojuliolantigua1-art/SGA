namespace SGA.Desktop.DTOs.Parada
{
    public sealed class ParadaPresentacionDto
    {
        public int Id { get; set; }
        public int RutaId { get; set; }
        public string? Nombre { get; set; }
        public string? Referencia { get; set; }
        public int Orden { get; set; }
    }

    public sealed class CrearParadaPresentacionDto
    {
        public int RutaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Referencia { get; set; }
        public int Orden { get; set; }
        public string? CreadoPor { get; set; }
    }
}
