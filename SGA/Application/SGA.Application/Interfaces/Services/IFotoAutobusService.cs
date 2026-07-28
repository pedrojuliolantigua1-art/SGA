using SGA.Application.DTOs.Fotos;
using SGA.Domain.Error;

namespace SGA.Application.Interfaces.Services
{
    public interface IFotoAutobusService
    {
        Task<Result<IReadOnlyList<FotoAutobusDto>>> ListarPorAutobusAsync(int autobusId);
        Task<Result<FotoAutobusDto>> SubirAsync(int autobusId, byte[] contenido, string nombreArchivo, string? subidoPor);
        Task<Result> EliminarAsync(int fotoId);
    }
}
