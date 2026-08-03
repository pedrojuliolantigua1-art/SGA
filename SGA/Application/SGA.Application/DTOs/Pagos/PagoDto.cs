using SGA.Domain.Enum;

namespace SGA.Application.DTOs.Pagos
{
    public sealed record PagoDto(
        int Id, int UsuarioTransporteId, int AutorizacionTransporteId, decimal Monto,
        string? TipoPago, EstadoPago Estado, DateTime FechaHora, int RegistradoPorUsuarioId);

    public sealed record RegistrarPagoDto(
        int UsuarioTransporteId, decimal Monto, string TipoPago,
         DateTime FechaHora, int RegistradoPorUsuarioId, string? CreadoPor);
}
