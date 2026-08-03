using SGA.Application.Common;
using SGA.Application.DTOs.Fotos;
using SGA.Application.Interfaces.Services;
using SGA.Domain.Entities.Fotos;
using SGA.Domain.Error;
using SGA.Domain.Models.Fotos;
using SGA.Domain.Repository.Interfaces;
using SGA.Domain.Services;

namespace SGA.Application.Services
{
    public sealed class FotoAutobusService : IFotoAutobusService
    {
        private readonly IFotoAutobusRepository _fotoRepository;
        private readonly IAutobusRepository _autobusRepository;
        private readonly IAlmacenamientoArchivos _almacenamiento;

        public FotoAutobusService(IFotoAutobusRepository fotoRepository, IAutobusRepository autobusRepository, IAlmacenamientoArchivos almacenamiento)
        {
            _fotoRepository = fotoRepository;
            _autobusRepository = autobusRepository;
            _almacenamiento = almacenamiento;
        }

        public async Task<Result<IReadOnlyList<FotoAutobusDto>>> ListarPorAutobusAsync(int autobusId)
        {
            var fotos = await _fotoRepository.GetAllByAutobusId(autobusId);
            return Result<IReadOnlyList<FotoAutobusDto>>.Ok(fotos.Select(MapearFoto).ToList());
        }

        public async Task<Result<FotoAutobusDto>> SubirAsync(
            int autobusId, byte[] contenido, string nombreArchivo, string? subidoPor)
        {
            var autobus = await _autobusRepository.GetByIdAsync(autobusId);
            if (autobus is null)
                return Result<FotoAutobusDto>.Fallo(ApplicationErrors.NoEncontrado("el autobus"));

            //sube el binario a Cloudinary
            var subida = await _almacenamiento.SubirAsync(contenido, nombreArchivo, "autobuses");

            // guardo ese registro en la base de datos
            var foto = new FotoAutobus
            {
                AutobusId = autobusId,
                NombreArchivo = subida.NombreArchivo,
                UrlPublica = subida.UrlPublica,
                PublicId = subida.PublicId,
                SubidoPor = subidoPor ?? "sistema",
                FechaSubida = DateTime.UtcNow
            };

            await _fotoRepository.AddAsync(foto);

            return Result<FotoAutobusDto>.Ok(
                new FotoAutobusDto(foto.Id, foto.AutobusId, foto.NombreArchivo, foto.UrlPublica, foto.FechaSubida));
        }

        public async Task<Result> EliminarAsync(int fotoId)
        {
            var foto = await _fotoRepository.GetByIdAsync(fotoId);
            if (foto is null)
                return Result.Fallo(ApplicationErrors.NoEncontrado("la foto"));

            //este para eliminar de manera fisica
            await _almacenamiento.EliminarAsync(foto.PublicId);

            //este para soft delete
            await _fotoRepository.DeleteAsync(new FotoAutobus { Id = fotoId });

            return Result.Ok();
        }

        private static FotoAutobusDto MapearFoto(FotoAutobusModel f)
            => new(f.Id, f.AutobusId, f.NombreArchivo, f.UrlPublica, f.FechaSubida);
    }
}
