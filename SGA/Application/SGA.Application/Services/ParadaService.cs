using SGA.Application.Common;
using SGA.Application.DTOs.Common;
using SGA.Application.DTOs.Paradas;
using SGA.Application.Interfaces.Services;
using SGA.Domain.Entities.Transporte;
using SGA.Domain.Error;
using SGA.Domain.Models.Transporte;
using SGA.Domain.Repository.Interfaces;
using SGA.Domain.Rules;

namespace SGA.Application.Services
{
    public sealed class ParadaService : IParadaService
    {
        private readonly IParadaRepository _paradaRepository;
        private readonly IViajeRepository _viajeRepository;
        private readonly IAuditoriaService _auditoriaService;
        private readonly ICurrentUserService _currentUser;

        public ParadaService(
            IParadaRepository paradaRepository, IViajeRepository viajeRepository,
            IAuditoriaService auditoriaService, ICurrentUserService currentUser)
        {
            _paradaRepository = paradaRepository;
            _viajeRepository = viajeRepository;
            _auditoriaService = auditoriaService;
            _currentUser = currentUser;
            
        }

        public async Task<Result<IReadOnlyList<ParadaDto>>> ListarPorRutaAsync(int rutaId)
        {
            var paradas = await _paradaRepository.GetByRuta(rutaId);
            return Result<IReadOnlyList<ParadaDto>>.Ok(paradas.Select(MapearParada).ToList());
        }


        public async Task<Result<ParadaDto>> ObtenerPorIdAsync(int paradaId)
        {
            var parada = await _paradaRepository.GetByIdAsync(paradaId);
            return parada is null
                ? Result<ParadaDto>.Fallo(ApplicationErrors.NoEncontrado("la parada"))
                : Result<ParadaDto>.Ok(MapearParada(parada));
        }

        public async Task<Result<ParadaDto>> CrearAsync(CrearParadaDto dto)
        {
            var parada = new Parada
            {
                RutaId = dto.RutaId,
                Nombre = dto.Nombre,
                Referencia = dto.Referencia,
                Orden = dto.Orden,
                CreadoPor = dto.CreadoPor
            };

            var validacion = ParadaRules.Validar(parada);
            if (validacion.EsFallo)
                return Result<ParadaDto>.Fallo(validacion.Error!);

            // RN: no puede haber dos paradas con el mismo orden dentro de la misma ruta.
            var paradasDeLaRuta = await _paradaRepository.GetByRuta(dto.RutaId);
            var candidatas = paradasDeLaRuta.Select(p => new Parada { Orden = p.Orden })
                .Append(new Parada { Orden = parada.Orden });

            var validacionOrden = ParadaRules.ValidarOrdenUnico(candidatas);
            if (validacionOrden.EsFallo)
                return Result<ParadaDto>.Fallo(validacionOrden.Error!);

            await _paradaRepository.AddAsync(parada);

            await _auditoriaService.RegistrarAsync(_currentUser.UsuarioId, "ParadaCreada", "Parada", parada.Id.ToString(), $"Parada {parada.Nombre} creada en la ruta {parada.RutaId}.");

            return Result<ParadaDto>.Ok(MapearParada(parada));
        }

        public async Task<Result<ParadaDto>> ActualizarAsync(int paradaId, ActualizarParadaDto dto)
        {
            var actual = await _paradaRepository.GetByIdAsync(paradaId);
            if (actual is null)
                return Result<ParadaDto>.Fallo(ApplicationErrors.NoEncontrado("la parada"));

            var parada = new Parada { Id = paradaId, RutaId = actual.RutaId, Nombre = dto.Nombre, Referencia = dto.Referencia, Orden = dto.Orden };

            var validacion = ParadaRules.Validar(parada);
            if (validacion.EsFallo)
                return Result<ParadaDto>.Fallo(validacion.Error!);

            var paradasDeLaRuta = await _paradaRepository.GetByRuta(actual.RutaId);
            var candidatas = paradasDeLaRuta.Where(p => p.Id != paradaId)
                .Select(p => new Parada { Orden = p.Orden })
                .Append(new Parada { Orden = parada.Orden });

            var validacionOrden = ParadaRules.ValidarOrdenUnico(candidatas);
            if (validacionOrden.EsFallo)
                return Result<ParadaDto>.Fallo(validacionOrden.Error!);

            await _paradaRepository.UpdateAsync(parada);
            return Result<ParadaDto>.Ok(MapearParada(parada));
        }

        public async Task<Result> ReordenarAsync(ReordenarParadasDto dto)
        {
            var paradasActuales = await _paradaRepository.GetByRuta(dto.RutaId);
            if (paradasActuales.Count != dto.Orden.Count)
                return Result.Fallo(ApplicationErrors.OperacionInvalida("El listado de orden no coincide con las paradas de la ruta."));

            var candidatas = paradasActuales.Select(p =>
            {
                var nuevoOrden = dto.Orden.First(o => o.ParadaId == p.Id).NuevoOrden;
                return new Parada { Id = p.Id, RutaId = p.RutaId, Nombre = p.Nombre, Referencia = p.Referencia, Orden = nuevoOrden };
            }).ToList();

            var validacion = ParadaRules.ValidarOrdenUnico(candidatas);
            if (validacion.EsFallo)
                return validacion;

            foreach (var parada in candidatas)
                await _paradaRepository.UpdateAsync(parada);

            return Result.Ok();
        }

        public async Task<Result> EliminarAsync(int paradaId, EliminarDto dto)
        {
            var actual = await _paradaRepository.GetByIdAsync(paradaId);
            if (actual is null)
                return Result.Fallo(ApplicationErrors.NoEncontrado("la parada"));

            // RN-OPE: toda ruta debe conservar al menos dos paradas.
            var paradasDeLaRuta = await _paradaRepository.GetByRuta(actual.RutaId);
            if (paradasDeLaRuta.Count <= 2)
                return Result.Fallo(ApplicationErrors.OperacionInvalida(
                    "No se puede eliminar: la ruta debe conservar al menos dos paradas."));

            // No se elimina una parada de una ruta con viajes programados o en curso —
            // el estudiante podría haber comprado un ticket contando con esa parada.
            var viajesDeLaRuta = await _viajeRepository.GetbyRuta(actual.RutaId);
            var tieneViajesActivos = viajesDeLaRuta.Any(v =>
                v.Estado == SGA.Domain.Enum.EstadoViaje.Programado ||
                v.Estado == SGA.Domain.Enum.EstadoViaje.EnCurso ||
                v.Estado == SGA.Domain.Enum.EstadoViaje.Retrasado);

            if (tieneViajesActivos)
                return Result.Fallo(ApplicationErrors.OperacionInvalida(
                    "No se puede eliminar: la ruta tiene viajes programados o en curso. Cancélalos primero."));

            var parada = new Parada { Id = paradaId, Eliminado = true, FechaEliminacion = DateTime.UtcNow, EliminadoPor = dto.EliminadoPor };
            await _paradaRepository.DeleteAsync(parada);

            await _auditoriaService.RegistrarAsync(_currentUser.UsuarioId, "ParadaEliminada", "Parada", paradaId.ToString(), $"Parada eliminada por {dto.EliminadoPor}.");

            return Result.Ok();
        }

        private static ParadaDto MapearParada(ParadaModel p) => new(
            p.Id,
            p.RutaId,
            p.Nombre,
            p.Referencia,
            p.Orden
        );

        private static ParadaDto MapearParada(Parada p) => new(
            p.Id,
            p.RutaId,
            p.Nombre,
            p.Referencia,
            p.Orden
        );
    }
}