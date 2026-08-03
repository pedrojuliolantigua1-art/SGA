using SGA.Domain.Enum;

namespace SGA.Application.DTOs.Viajes
{
    public sealed record ViajeDto(
        int Id, int RutaId, int HorarioRutaId, int AutobusId, int ConductorId,
        DateTime Fecha, EstadoViaje Estado, DateTime? HoraInicioReal, DateTime? HoraFinReal,
        int CupoActual, int CapacidadMaxima);

    public sealed record ProgramarViajeDto(
        int RutaId, int HorarioRutaId, int AutobusId, int ConductorId,
        DateTime Fecha, string? CreadoPor);

    public sealed record EjecutarViajeDto(
        int ViajeId, int ConductorId, DateTime FechaHora);

    public sealed record CancelarViajeDto(
        int ViajeId, string Motivo, string? CreadoPor);

    public sealed record IncidenciaDto(
        int Id, int ViajeId, int ConductorId, string? Tipo, string? Descripcion, DateTime FechaHora,
        string? ConductorNombre = null);

    public sealed record ReportarIncidenciaDto(
        int ViajeId, int ConductorId, string Tipo, string Descripcion,
        DateTime FechaHora, string? CreadoPor, bool EsAdmin = false);

    /// <summary>
    /// Programa el mismo viaje (misma ruta/horario/autobús/conductor) para varios días de UNA semana,
    /// en una sola operación. Cada día queda como un Viaje independiente — se puede editar, cancelar
    /// o cambiar de autobús/conductor un día puntual sin afectar a los demás.
    /// </summary>
    public sealed record ProgramarSemanaDto(
        int RutaId, int HorarioRutaId, int AutobusId, int ConductorId,
        DateTime FechaReferenciaSemana, List<DayOfWeek> Dias, string? CreadoPor);

    public sealed record ProgramarSemanaResultadoDto(
        IReadOnlyList<ViajeDto> Creados, IReadOnlyList<string> Errores);
}
