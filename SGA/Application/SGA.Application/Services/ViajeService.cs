using SGA.Application.Common;
using SGA.Application.DTOs.Notificaciones;
using SGA.Application.DTOs.Viajes;
using SGA.Application.Interfaces.Services;
using SGA.Domain.Entities.Transporte;
using SGA.Domain.Entities.Viajes;
using SGA.Domain.Enum;
using SGA.Domain.Error;
using SGA.Domain.Models.Transporte;
using SGA.Domain.Models.Usuarios;
using SGA.Domain.Models.Viajes;
using SGA.Domain.Repository.Interfaces;
using SGA.Domain.Rules;
using SGA.Domain.Validation;

namespace SGA.Application.Services
{
    public sealed class ViajeService : IViajeService
    {
        private readonly IViajeRepository _viajeRepository;
        private readonly IRutaRepository _rutaRepository;
        private readonly IHorarioRutaRepository _horarioRutaRepository;
        private readonly IAutobusRepository _autobusRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAccesoRepository _accesoRepository;
        private readonly IAuditoriaService _auditoriaService;
        private readonly INotificacionService _notificacionService;
        private readonly ICurrentUserService _currentUser;

        public ViajeService(
            IViajeRepository viajeRepository,
            IRutaRepository rutaRepository,
            IHorarioRutaRepository horarioRutaRepository,
            IAutobusRepository autobusRepository,
            IUsuarioRepository usuarioRepository,
            IAccesoRepository accesoRepository,
            IAuditoriaService auditoriaService,
            INotificacionService notificacionService,
            ICurrentUserService currentUser
            )
        {
            _viajeRepository = viajeRepository;
            _rutaRepository = rutaRepository;
            _horarioRutaRepository = horarioRutaRepository;
            _autobusRepository = autobusRepository;
            _usuarioRepository = usuarioRepository;
            _accesoRepository = accesoRepository;
            _auditoriaService = auditoriaService;
            _notificacionService = notificacionService;
            _currentUser = currentUser;
        }

        public async Task<Result<IReadOnlyList<ViajeDto>>> ListarPorFechaAsync(DateTime fecha)
        {
            var validacion = ValidationGeneral.FechaDefinida(fecha, "viaje");

            if (validacion.EsFallo)
            {
                return Result<IReadOnlyList<ViajeDto>>.Fallo(validacion.Error!);
            }

            var viajes = await _viajeRepository.GetbyFecha(fecha.Date);
            return Result<IReadOnlyList<ViajeDto>>.Ok(viajes.Select(MapearViaje).ToList());
        }

        public async Task<Result<IReadOnlyList<ViajeDto>>> ListarPorConductorAsync(int conductorId)
        {
            var validacion = ValidationGeneral.IdValido(conductorId, "conductor");

            if (validacion.EsFallo)
            {
                return Result<IReadOnlyList<ViajeDto>>.Fallo(validacion.Error!);
            }

            var viajes = await _viajeRepository.GetbyConductor(conductorId);
            return Result<IReadOnlyList<ViajeDto>>.Ok(viajes.Select(MapearViaje).ToList());
        }

        public async Task<Result<ViajeDto>> ObtenerPorIdAsync(int viajeId)
        {
            var validacion = ValidationGeneral.IdValido(viajeId, "viaje");
            if (validacion.EsFallo)
                return Result<ViajeDto>.Fallo(validacion.Error!);

            var viaje = await _viajeRepository.GetByIdAsync(viajeId);
            return viaje is null
                ? Result<ViajeDto>.Fallo(ApplicationErrors.NoEncontrado("el viaje"))
                : Result<ViajeDto>.Ok(MapearViaje(viaje));
        }

        public async Task<Result<IReadOnlyList<ViajeDto>>> ListarActivosAsync()
        {
            var viajes = await _viajeRepository.GetActivos();
            return Result<IReadOnlyList<ViajeDto>>.Ok(viajes.Select(MapearViaje).ToList());
        }

        public async Task<Result<IReadOnlyList<ViajeDto>>> ListarProgramadosAsync()
        {
            var viajes = await _viajeRepository.GetProgramados();
            return Result<IReadOnlyList<ViajeDto>>.Ok(viajes.Select(MapearViaje).ToList());
        }

        public async Task<Result<IReadOnlyList<ViajeDto>>> ListarPorRutaAsync(int rutaId)
        {
            var validacion = ValidationGeneral.IdValido(rutaId, "ruta");
            if (validacion.EsFallo)
                return Result<IReadOnlyList<ViajeDto>>.Fallo(validacion.Error!);

            var viajes = await _viajeRepository.GetbyRuta(rutaId);
            return Result<IReadOnlyList<ViajeDto>>.Ok(viajes.Select(MapearViaje).ToList());
        }

        public async Task<Result<IReadOnlyList<ViajeDto>>> ListarPorAutobusAsync(int autobusId)
        {
            var validacion = ValidationGeneral.IdValido(autobusId, "autobus");
            if (validacion.EsFallo)
                return Result<IReadOnlyList<ViajeDto>>.Fallo(validacion.Error!);

            var viajes = await _viajeRepository.GetbyAutobus(autobusId);
            return Result<IReadOnlyList<ViajeDto>>.Ok(viajes.Select(MapearViaje).ToList());
        }

        public async Task<Result<ProgramarSemanaResultadoDto>> ProgramarSemanaAsync(ProgramarSemanaDto dto)
        {
            if (dto.Dias is null || dto.Dias.Count == 0)
                return Result<ProgramarSemanaResultadoDto>.Fallo(
                    ApplicationErrors.OperacionInvalida("Selecciona al menos un dia de la semana."));

            // Lunes de la semana que contiene FechaReferenciaSemana.
            var diaDeLaSemana = (int)dto.FechaReferenciaSemana.DayOfWeek;
            var offsetHastaLunes = diaDeLaSemana == 0 ? 6 : diaDeLaSemana - 1;
            var lunes = dto.FechaReferenciaSemana.Date.AddDays(-offsetHastaLunes);

            var creados = new List<ViajeDto>();
            var errores = new List<string>();

            foreach (var dia in dto.Dias.Distinct())
            {
                var offsetDelDia = (int)dia == 0 ? 6 : (int)dia - 1;
                var fecha = lunes.AddDays(offsetDelDia);

                var resultado = await ProgramarAsync(new ProgramarViajeDto(
                    dto.RutaId, dto.HorarioRutaId, dto.AutobusId, dto.ConductorId, fecha, dto.CreadoPor));

                if (resultado.EsExitoso)
                    creados.Add(resultado.Valor!);
                else
                    errores.Add($"{fecha:dd/MM} ({dia}): {resultado.Error}");
            }

            return Result<ProgramarSemanaResultadoDto>.Ok(new ProgramarSemanaResultadoDto(creados, errores));
        }

        public async Task<Result<ViajeDto>> ProgramarAsync(ProgramarViajeDto dto)
        {
            var datosValidos = ValidationGeneral.Combinar(
                ValidationGeneral.IdValido(dto.RutaId, "ruta"),
                ValidationGeneral.IdValido(dto.HorarioRutaId, "horario"),
                ValidationGeneral.IdValido(dto.AutobusId, "autobus"),
                ValidationGeneral.IdValido(dto.ConductorId, "conductor"),
                ValidationGeneral.FechaDefinida(dto.Fecha, "viaje"));

            if (datosValidos.EsFallo)
            {
                return Result<ViajeDto>.Fallo(datosValidos.Error!);
            }

            var rutaModel = await _rutaRepository.GetByIdAsync(dto.RutaId);
            var horarioModel = await _horarioRutaRepository.GetByIdAsync(dto.HorarioRutaId);
            var autobusModel = await _autobusRepository.GetByIdAsync(dto.AutobusId);
            var conductorModel = await _usuarioRepository.GetByIdAsync(dto.ConductorId);

            if (rutaModel is null)
            {
                return Result<ViajeDto>.Fallo(ApplicationErrors.NoEncontrado("la ruta"));
            }

            if (horarioModel is null)
            {
                return Result<ViajeDto>.Fallo(ApplicationErrors.NoEncontrado("el horario"));
            }

            if (autobusModel is null)
            {
                return Result<ViajeDto>.Fallo(ApplicationErrors.NoEncontrado("el autobus"));
            }

            if (conductorModel is null)
            {
                return Result<ViajeDto>.Fallo(ApplicationErrors.NoEncontrado("el conductor"));
            }

            var viajesDelDia = (await _viajeRepository.GetbyFecha(dto.Fecha.Date))
                .Select(ConvertirViaje)
                .ToList();

            var viajeCreado = ViajePlanificacionRules.Crear(
                ConvertirRuta(rutaModel),
                ConvertirHorario(horarioModel),
                ConvertirAutobus(autobusModel),
                ConvertirConductor(conductorModel),
                dto.Fecha,
                viajesDelDia);

            if (viajeCreado.EsFallo)
            {
                return Result<ViajeDto>.Fallo(viajeCreado.Error!);
            }

            var viaje = viajeCreado.Valor!;
            viaje.FechaCreacion = DateTime.UtcNow;
            viaje.CreadoPor = dto.CreadoPor;

            await _viajeRepository.AddAsync(viaje);
            await _auditoriaService.RegistrarAsync(_currentUser.UsuarioId, "ViajeProgramado", "Viaje", viaje.Id.ToString(), $"Viaje programado para la ruta {dto.RutaId} el {dto.Fecha:d}, conductor {dto.ConductorId}, autobus {dto.AutobusId}.");
            return Result<ViajeDto>.Ok(MapearViaje(viaje));
        }

        public async Task<Result<ViajeDto>> IniciarAsync(EjecutarViajeDto dto)
        {
            var datosValidos = ValidationGeneral.Combinar(
                ValidationGeneral.IdValido(dto.ViajeId, "viaje"),
                ValidationGeneral.IdValido(dto.ConductorId, "conductor"));

            if (datosValidos.EsFallo)
            {
                return Result<ViajeDto>.Fallo(datosValidos.Error!);
            }

            var viajeModel = await _viajeRepository.GetByIdAsync(dto.ViajeId);

            if (viajeModel is null)
            {
                return Result<ViajeDto>.Fallo(ApplicationErrors.NoEncontrado("el viaje"));
            }

            var otroViajeEnCurso = (await _viajeRepository.GetbyConductor(dto.ConductorId))
                .FirstOrDefault(v =>
                    v.Id != dto.ViajeId &&
                    (v.Estado == EstadoViaje.EnCurso ||
                     v.Estado == EstadoViaje.Retrasado && v.HoraInicioReal is not null));

            if (otroViajeEnCurso is not null)
            {
                return Result<ViajeDto>.Fallo(ApplicationErrors.OperacionInvalida(
                    $"El conductor ya tiene el viaje #{otroViajeEnCurso.Id} en curso. Debe finalizarlo antes de iniciar otro."));
            }

            var viaje = ConvertirViaje(viajeModel);
            var validacion = ViajeEjecucionRules.Iniciar(viaje, dto.ConductorId, dto.FechaHora);

            if (validacion.EsFallo)
            {
                return Result<ViajeDto>.Fallo(validacion.Error!);
            }

            viaje.FechaModificacion = DateTime.UtcNow;

            await _viajeRepository.UpdateAsync(viaje);
            await _auditoriaService.RegistrarAsync(dto.ConductorId, "ViajeIniciado", "Viaje", dto.ViajeId.ToString(), $"El conductor {dto.ConductorId} inicio el viaje.");
            await NotificarUsuariosVinculadosAsync(
                viaje.Id,
                "ViajeIniciado",
                $"Viaje #{viaje.Id} iniciado",
                $"Tu viaje #{viaje.Id} ya inicio. Puedes preparar tu abordaje.",
                dto.FechaHora,
                $"Conductor {dto.ConductorId}");

            return Result<ViajeDto>.Ok(MapearViaje(viaje));
        }

        public async Task<Result<ViajeDto>> FinalizarAsync(EjecutarViajeDto dto)
        {
            var datosValidos = ValidationGeneral.Combinar(
                ValidationGeneral.IdValido(dto.ViajeId, "viaje"),
                ValidationGeneral.IdValido(dto.ConductorId, "conductor"));

            if (datosValidos.EsFallo)
            {
                return Result<ViajeDto>.Fallo(datosValidos.Error!);
            }

            var viajeModel = await _viajeRepository.GetByIdAsync(dto.ViajeId);

            if (viajeModel is null)
            {
                return Result<ViajeDto>.Fallo(ApplicationErrors.NoEncontrado("el viaje"));
            }

            var viaje = ConvertirViaje(viajeModel);
            var validacion = ViajeEjecucionRules.Finalizar(viaje, dto.ConductorId, dto.FechaHora);

            if (validacion.EsFallo)
            {
                return Result<ViajeDto>.Fallo(validacion.Error!);
            }

            viaje.FechaModificacion = DateTime.UtcNow;

            await _viajeRepository.UpdateAsync(viaje);
            await _auditoriaService.RegistrarAsync(dto.ConductorId, "ViajeFinalizado", "Viaje", dto.ViajeId.ToString(), $"El conductor {dto.ConductorId} finalizo el viaje.");
            return Result<ViajeDto>.Ok(MapearViaje(viaje));
        }

        public async Task<Result<ViajeDto>> CancelarAsync(CancelarViajeDto dto)
        {
            var idValido = ValidationGeneral.IdValido(dto.ViajeId, "viaje");

            if (idValido.EsFallo)
            {
                return Result<ViajeDto>.Fallo(idValido.Error!);
            }

            var viajeModel = await _viajeRepository.GetByIdAsync(dto.ViajeId);

            if (viajeModel is null)
            {
                return Result<ViajeDto>.Fallo(ApplicationErrors.NoEncontrado("el viaje"));
            }

            var viaje = ConvertirViaje(viajeModel);
            var validacion = ViajeEjecucionRules.Cancelar(viaje, dto.Motivo);

            if (validacion.EsFallo)
            {
                return Result<ViajeDto>.Fallo(validacion.Error!);
            }

            viaje.FechaModificacion = DateTime.UtcNow;

            await _viajeRepository.UpdateAsync(viaje);
            await _auditoriaService.RegistrarAsync(viaje.ConductorId, "ViajeCancelado", "Viaje", dto.ViajeId.ToString(), $"Viaje cancelado. Motivo: {dto.Motivo}");
            return Result<ViajeDto>.Ok(MapearViaje(viaje));
        }

        public async Task<Result<IncidenciaDto>> ReportarIncidenciaAsync(ReportarIncidenciaDto dto)
        {
            var datosValidos = ValidationGeneral.Combinar(
                ValidationGeneral.IdValido(dto.ViajeId, "viaje"),
                ValidationGeneral.IdValido(dto.ConductorId, "conductor"));

            if (datosValidos.EsFallo)
            {
                return Result<IncidenciaDto>.Fallo(datosValidos.Error!);
            }

            var viajeModel = await _viajeRepository.GetByIdAsync(dto.ViajeId);

            if (viajeModel is null)
            {
                return Result<IncidenciaDto>.Fallo(ApplicationErrors.NoEncontrado("el viaje"));
            }

            var viaje = ConvertirViaje(viajeModel);
            var estadoAnterior = viaje.Estado;
            var incidenciaCreada = ViajeEjecucionRules.ReportarIncidencia(
                viaje,
                dto.ConductorId,
                dto.Tipo,
                dto.Descripcion,
                dto.FechaHora,
                validarConductor: !dto.EsAdmin);

            if (incidenciaCreada.EsFallo)
            {
                return Result<IncidenciaDto>.Fallo(incidenciaCreada.Error!);
            }

            var incidencia = incidenciaCreada.Valor!;
            if (dto.EsAdmin)
            {
                // El administrador registra la incidencia a nombre del conductor del viaje.
                incidencia.ConductorId = viaje.ConductorId;
            }

            incidencia.CreadoPor = dto.CreadoPor;
            incidencia.FechaCreacion = DateTime.UtcNow;

            if (viaje.Estado != estadoAnterior)
            {
                viaje.FechaModificacion = DateTime.UtcNow;
                await _viajeRepository.UpdateAsync(viaje);
            }

            await _viajeRepository.AddIncidencia(incidencia);
            await _auditoriaService.RegistrarAsync(dto.ConductorId, "IncidenciaReportada", "Viaje", dto.ViajeId.ToString(), $"Incidencia ({dto.Tipo}) reportada por {dto.CreadoPor ?? $"usuario {dto.ConductorId}"}: {dto.Descripcion}");

            var notificadoPor = dto.CreadoPor ?? (dto.EsAdmin ? "Administrador" : $"Conductor {dto.ConductorId}");

            await NotificarUsuariosVinculadosAsync(
                incidencia.ViajeId,
                "IncidenciaViaje",
                $"Incidencia en viaje #{incidencia.ViajeId}",
                $"{incidencia.Tipo}: {incidencia.Descripcion}",
                incidencia.FechaHora,
                notificadoPor);

            await NotificarAdministradoresAsync(
                "IncidenciaViaje",
                $"Incidencia en viaje #{incidencia.ViajeId}",
                $"{incidencia.Tipo}: {incidencia.Descripcion}",
                incidencia.FechaHora,
                notificadoPor);

            return Result<IncidenciaDto>.Ok(MapearIncidencia(incidencia));
        }

        private async Task NotificarUsuariosVinculadosAsync(
            int viajeId,
            string tipo,
            string titulo,
            string mensaje,
            DateTime fechaHora,
            string creadoPor)
        {
            var accesos = await _accesoRepository.GetByViaje(viajeId);
            var usuarios = accesos
                .Where(a => a.ResultadoAcceso == ResultadoAcceso.Permitido)
                .Select(a => a.UsuarioTransporteId)
                .Distinct()
                .ToList();

            foreach (var usuarioId in usuarios)
            {
                await _notificacionService.CrearAsync(new CrearNotificacionDto(
                    usuarioId,
                    tipo,
                    titulo,
                    mensaje,
                    fechaHora,
                    creadoPor));
            }
        }

        private async Task NotificarAdministradoresAsync(
            string tipo,
            string titulo,
            string mensaje,
            DateTime fechaHora,
            string creadoPor)
        {
            var idsAdministradores = await _usuarioRepository.ObtenerIdsPorRol(RolUsuario.AdministradorTransporte);

            foreach (var adminId in idsAdministradores)
            {
                await _notificacionService.CrearAsync(new CrearNotificacionDto(
                    adminId,
                    tipo,
                    titulo,
                    mensaje,
                    fechaHora,
                    creadoPor));
            }
        }

        private static Ruta ConvertirRuta(RutaModel model) => new()
        {
            Id = model.Id,
            Nombre = model.Nombre,
            Descripcion = model.Descripcion,
            Activa = model.Activa
        };

        private static HorarioRuta ConvertirHorario(HorarioModel model) => new()
        {
            Id = model.Id,
            RutaId = model.RutaId,
            HoraSalida = model.HoraSalida,
            HoraLlegadaEstimada = model.HoraLlegadaEstimada,
            Activo = model.Activo
        };

        private static Autobus ConvertirAutobus(AutobusModel model) => new()
        {
            Id = model.Id,
            Placa = model.Placa,
            Marca = model.Marca,
            Modelo = model.Modelo,
            Capacidad = model.Capacidad,
            Estado = model.Estado
        };

        private static Conductor? ConvertirConductor(UsuarioModel model)
        {
            if (model is not ConductorModel conductor)
            {
                return null;
            }

            return new Conductor
            {
                Id = conductor.Id,
                Nombre = conductor.Nombre,
                Apellido = conductor.Apellido,
                Correo = conductor.Correo,
                Telefono = conductor.Telefono,
                Estado = conductor.Estado,
                RolSistema = conductor.RolSistema,
                TipoUsuario = "Conductor",
                NumeroLicencia = conductor.NumeroLicencia,
                FechaVencimientoLicencia = conductor.FechaVencimientoLicencia,
                Disponible = conductor.Disponible
            };
        }

        private static Viaje ConvertirViaje(ViajeModel model) => new()
        {
            Id = model.Id,
            RutaId = model.RutaId,
            HorarioRutaId = model.HorarioRutaId,
            AutobusId = model.AutobusId,
            ConductorId = model.ConductorId,
            Fecha = model.Fecha,
            Estado = model.Estado,
            HoraInicioReal = model.HoraInicioReal,
            HoraFinReal = model.HoraFinReal,
            CupoActual = model.CupoActual,
            CapacidadMaxima = model.CapacidadMaxima
        };

        private static ViajeDto MapearViaje(ViajeModel viaje) =>
            new(
                viaje.Id,
                viaje.RutaId,
                viaje.HorarioRutaId,
                viaje.AutobusId,
                viaje.ConductorId,
                viaje.Fecha,
                viaje.Estado,
                viaje.HoraInicioReal,
                viaje.HoraFinReal,
                viaje.CupoActual,
                viaje.CapacidadMaxima);

        private static ViajeDto MapearViaje(Viaje viaje) =>
            new(
                viaje.Id,
                viaje.RutaId,
                viaje.HorarioRutaId,
                viaje.AutobusId,
                viaje.ConductorId,
                viaje.Fecha,
                viaje.Estado,
                viaje.HoraInicioReal,
                viaje.HoraFinReal,
                viaje.CupoActual,
                viaje.CapacidadMaxima);

        private static IncidenciaDto MapearIncidencia(Incidencia incidencia) =>
            new(
                incidencia.Id,
                incidencia.ViajeId,
                incidencia.ConductorId,
                incidencia.Tipo,
                incidencia.Descripcion,
                incidencia.FechaHora);

        private static IncidenciaDto MapearIncidencia(IncidenciaModel incidencia) =>
            new(
                incidencia.Id,
                incidencia.ViajeId,
                incidencia.ConductorId,
                incidencia.Tipo,
                incidencia.Descripcion,
                incidencia.FechaHora,
                incidencia.ConductorNombre);

        public async Task<Result<IReadOnlyList<IncidenciaDto>>> ListarIncidenciasPorPeriodoAsync(DateTime desde, DateTime hasta)
        {
            var incidencias = await _viajeRepository.GetIncidenciasbyPeriodo(desde, hasta);
            return Result<IReadOnlyList<IncidenciaDto>>.Ok(incidencias.Select(MapearIncidencia).ToList());
        }
    }
}
