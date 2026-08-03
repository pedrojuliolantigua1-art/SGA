using SGA.Desktop.DTOs.Horario;
using SGA.Desktop.DTOs.Parada;

namespace SGA.Desktop.DTOs.Ruta
{
    public sealed class RutaPresentacionDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Activa { get; set; }
    }

    public sealed class CrearRutaPresentacionDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Activa { get; set; } = true;
        public string? CreadoPor { get; set; }
    }

    public sealed class ActualizarRutaPresentacionDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Activa { get; set; }
    }

    public sealed class RutaDetallePresentacionDto
    {
        public RutaPresentacionDto Ruta { get; set; } = new();
        public List<ParadaPresentacionDto> Paradas { get; set; } = new();
        public List<HorarioRutaPresentacionDto> Horarios { get; set; } = new();
    }
}
