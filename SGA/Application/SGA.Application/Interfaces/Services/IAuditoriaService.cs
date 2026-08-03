using SGA.Application.DTOs.Auditoria;
using SGA.Domain.Error;

namespace SGA.Application.Interfaces.Services
{
    public interface IAuditoriaService
    {
        Task<Result<IReadOnlyList<AuditoriaDto>>> ListarPorPeriodoAsync(DateTime desde, DateTime hasta);

        Task<Result<AuditoriaDto>> ObtenerPorIdAsync(int registroId);
        Task<Result<IReadOnlyList<AuditoriaDto>>> ListarPorActorAsync(int usuarioId);
        Task<Result<IReadOnlyList<AuditoriaDto>>> ListarPorAccionAsync(string accion);

        /// <summary>
        /// Registra un evento de auditoria. Debe llamarse desde cualquier servicio que realice
        /// una accion relevante (creaciones, actualizaciones, bajas, accesos, pagos, etc.),
        /// tal como exige la regla de negocio RN-AUD del SRS.
        /// </summary>
        Task<Result> RegistrarAsync(int usuarioTransporteId, string accion, string entidadAfectada, string entidadId, string detalle);
    }
}