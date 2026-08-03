using SGA.Application.Common;
using SGA.Application.DTOs.Common;
using SGA.Application.DTOs.Usuarios;
using SGA.Application.Interfaces.Services;
using SGA.Domain.Entities.Transporte;
using SGA.Domain.Entities.Usuarios;
using SGA.Domain.Error;
using SGA.Domain.Models.Usuarios;
using SGA.Domain.Repository.Interfaces;
using SGA.Domain.Rules;

namespace SGA.Application.Services
{
    public sealed class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuditoriaService _auditoriaService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IAuditoriaService auditoriaService)
        {
            _usuarioRepository = usuarioRepository;
            _auditoriaService = auditoriaService;
        }

        public async Task<Result<IReadOnlyList<UsuarioResumenDto>>> ListarTodosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Result<IReadOnlyList<UsuarioResumenDto>>.Ok(usuarios.Select(MapearResumen).ToList());
        }

        public async Task<Result<UsuarioResumenDto>> ObtenerPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario is null
                ? Result<UsuarioResumenDto>.Fallo(ApplicationErrors.NoEncontrado("el usuario"))
                : Result<UsuarioResumenDto>.Ok(MapearResumen(usuario));
        }

        public async Task<Result<UsuarioResumenDto>> ObtenerPorCorreoAsync(string correo)
        {
            var validacion = UsuarioBaseRules.ValidarCorreoInstitucional(correo);
            if (validacion.EsFallo)
                return Result<UsuarioResumenDto>.Fallo(validacion.Error!);

            var usuario = await _usuarioRepository.GetbyCorreo(correo);
            return usuario is null
                ? Result<UsuarioResumenDto>.Fallo(ApplicationErrors.NoEncontrado("el usuario"))
                : Result<UsuarioResumenDto>.Ok(MapearResumen(usuario));
        }

        public async Task<Result<EstudianteDto>> ObtenerEstudiantePorMatriculaAsync(string matricula)
        {
            var usuario = await _usuarioRepository.GetByMatricula(matricula);
            return usuario is not EstudianteModel estudiante
                ? Result<EstudianteDto>.Fallo(ApplicationErrors.NoEncontrado("el estudiante"))
                : Result<EstudianteDto>.Ok(new EstudianteDto(
                    estudiante.Id, estudiante.Nombre, estudiante.Apellido, estudiante.Correo, estudiante.Telefono,
                    estudiante.Estado, estudiante.RolSistema, estudiante.Matricula, estudiante.Carrera));
        }

        public async Task<Result<ConductorDto>> ObtenerConductorPorLicenciaAsync(string numeroLicencia)
        {
            var usuario = await _usuarioRepository.GetByNumeroLicencia(numeroLicencia);
            return usuario is not ConductorModel conductor
                ? Result<ConductorDto>.Fallo(ApplicationErrors.NoEncontrado("el conductor"))
                : Result<ConductorDto>.Ok(MapearConductor(conductor));
        }

        public async Task<Result<bool>> ValidarPasswordAsync(AutenticarDto dto)
        {
            var validacion = UsuarioBaseRules.ValidarCorreoInstitucional(dto.Correo);
            if (validacion.EsFallo)
                return Result<bool>.Fallo(validacion.Error!);

            var hashGuardado = await _usuarioRepository.ObtenerPasswordHashPorCorreoAsync(dto.Correo);
            var valido = SGA.Domain.Common.PasswordHasher.Verificar(dto.PasswordHash, hashGuardado);
            return Result<bool>.Ok(valido);
        }

        public async Task<Result<IReadOnlyList<ConductorDto>>> ListarConductoresAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var conductores = usuarios.OfType<ConductorModel>().Select(MapearConductor).ToList();
            return Result<IReadOnlyList<ConductorDto>>.Ok(conductores);
        }

        public async Task<Result<ConductorDto>> ObtenerConductorPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario is not ConductorModel conductor
                ? Result<ConductorDto>.Fallo(ApplicationErrors.NoEncontrado("el conductor"))
                : Result<ConductorDto>.Ok(MapearConductor(conductor));
        }

        public async Task<Result<EstudianteDto>> RegistrarEstudianteAsync(CrearEstudianteDto dto)
        {
            var estudiante = new Estudiante
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                PasswordHash = SGA.Domain.Common.PasswordHasher.Hash(dto.PasswordHash ?? string.Empty),
                Matricula = dto.Matricula,
                Carrera = dto.Carrera,
                TipoUsuario = "Estudiante",
                RolSistema = SGA.Domain.Enum.RolUsuario.Estudiante,
                CreadoPor = dto.CreadoPor
            };

            var validacion = EstudianteRules.Validar(estudiante);
            if (validacion.EsFallo)
                return Result<EstudianteDto>.Fallo(validacion.Error!);

            var matriculaExistente = await _usuarioRepository.GetByMatricula(dto.Matricula);
            if (matriculaExistente is not null)
                return Result<EstudianteDto>.Fallo(DomainErrors.Usuarios.Matricula);

            var existente = await _usuarioRepository.GetbyCorreo(dto.Correo ?? string.Empty);
            if (existente is not null)
                return Result<EstudianteDto>.Fallo(DomainErrors.Usuarios.CorreoDuplicado);

            await _usuarioRepository.AddAsync(estudiante);
            await _auditoriaService.RegistrarAsync(estudiante.Id, "EstudianteRegistrado", "Usuario", estudiante.Id.ToString(), $"Estudiante registrado: {estudiante.Nombre} {estudiante.Apellido} (matricula {estudiante.Matricula})");
            return Result<EstudianteDto>.Ok(MapearEstudiante(estudiante));
        }

        public async Task<Result<EstudianteDto>> ActualizarEstudianteAsync(int id, ActualizarEstudianteDto dto)
        {
            var actual = await _usuarioRepository.GetByIdAsync(id);
            if (actual is not EstudianteModel)
                return Result<EstudianteDto>.Fallo(ApplicationErrors.NoEncontrado("el estudiante"));

            var estudiante = new Estudiante
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Matricula = dto.Matricula,
                Carrera = dto.Carrera,
                TipoUsuario = "Estudiante",
                Estado = actual.Estado,
                RolSistema = actual.RolSistema
            };

            var validacion = EstudianteRules.Validar(estudiante);
            if (validacion.EsFallo)
                return Result<EstudianteDto>.Fallo(validacion.Error!);

            var existente = await _usuarioRepository.GetbyCorreo(dto.Correo ?? string.Empty);
            if (existente is not null && existente.Id != id)
                return Result<EstudianteDto>.Fallo(DomainErrors.Usuarios.CorreoDuplicado);

            await _usuarioRepository.UpdateAsync(estudiante);
            return Result<EstudianteDto>.Ok(MapearEstudiante(estudiante));
        }

        public async Task<Result<EmpleadoDocenteDto>> RegistrarEmpleadoDocenteAsync(CrearEmpleadoDocenteDto dto)
        {
            var docente = new EmpleadoDocente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                PasswordHash = SGA.Domain.Common.PasswordHasher.Hash(dto.PasswordHash ?? string.Empty),
                CodigoEmpleado = dto.CodigoEmpleado,
                Departamento = dto.Departamento,
                Cargo = dto.Cargo,
                Especialidad = dto.Especialidad,
                TipoContrato = dto.TipoContrato,
                TipoUsuario = "EmpleadoDocente",
                RolSistema = SGA.Domain.Enum.RolUsuario.Empleado,
                CreadoPor = dto.CreadoPor
            };

            var validacion = EmpleadoRules.Validar(docente);
            if (validacion.EsFallo)
                return Result<EmpleadoDocenteDto>.Fallo(validacion.Error!);

            var existente = await _usuarioRepository.GetbyCorreo(dto.Correo ?? string.Empty);
            if (existente is not null)
                return Result<EmpleadoDocenteDto>.Fallo(DomainErrors.Usuarios.CorreoDuplicado);

            var existenteCodigo = await _usuarioRepository.GetByCodigoEmpleado(dto.CodigoEmpleado ?? string.Empty);
            if (existenteCodigo is not null)
                return Result<EmpleadoDocenteDto>.Fallo(DomainErrors.Usuarios.CodigoEmpleadoDuplicado);

            await _usuarioRepository.AddAsync(docente);
            await _auditoriaService.RegistrarAsync(docente.Id, "EmpleadoDocenteRegistrado", "Usuario", docente.Id.ToString(), $"Empleado docente registrado: {docente.Nombre} {docente.Apellido}");
            return Result<EmpleadoDocenteDto>.Ok(MapearEmpleadoDocente(docente));
        }

        public async Task<Result<EmpleadoDocenteDto>> ActualizarEmpleadoDocenteAsync(int id, ActualizarEmpleadoDocenteDto dto)
        {
            var actual = await _usuarioRepository.GetByIdAsync(id);
            if (actual is not EmpleadoDocenteModel)
                return Result<EmpleadoDocenteDto>.Fallo(ApplicationErrors.NoEncontrado("el empleado docente"));

            var docente = new EmpleadoDocente
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                CodigoEmpleado = dto.CodigoEmpleado,
                Departamento = dto.Departamento,
                Cargo = dto.Cargo,
                Especialidad = dto.Especialidad,
                TipoContrato = dto.TipoContrato,
                TipoUsuario = "EmpleadoDocente",
                Estado = actual.Estado,
                RolSistema = actual.RolSistema
            };

            var validacion = EmpleadoRules.Validar(docente);
            if (validacion.EsFallo)
                return Result<EmpleadoDocenteDto>.Fallo(validacion.Error!);

            var existente = await _usuarioRepository.GetbyCorreo(dto.Correo ?? string.Empty);
            if (existente is not null && existente.Id != id)
                return Result<EmpleadoDocenteDto>.Fallo(DomainErrors.Usuarios.CorreoDuplicado);

            var existenteCodigo = await _usuarioRepository.GetByCodigoEmpleado(dto.CodigoEmpleado ?? string.Empty);
            if (existenteCodigo is not null && existenteCodigo.Id != id)
                return Result<EmpleadoDocenteDto>.Fallo(DomainErrors.Usuarios.CodigoEmpleadoDuplicado);

            await _usuarioRepository.UpdateAsync(docente);
            return Result<EmpleadoDocenteDto>.Ok(MapearEmpleadoDocente(docente));
        }

        public async Task<Result<EmpleadoAdministrativoDto>> RegistrarEmpleadoAdministrativoAsync(CrearEmpleadoAdministrativoDto dto)
        {
            var administrativo = new EmpleadoAdministrativo
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                PasswordHash = SGA.Domain.Common.PasswordHasher.Hash(dto.PasswordHash ?? string.Empty),
                CodigoEmpleado = dto.CodigoEmpleado,
                Departamento = dto.Departamento,
                Cargo = dto.Cargo,
                AreaAdministrativa = dto.AreaAdministrativa,
                TipoUsuario = "EmpleadoAdministrativo",
                RolSistema = SGA.Domain.Enum.RolUsuario.Empleado,
                CreadoPor = dto.CreadoPor
            };

            var validacion = EmpleadoRules.Validar(administrativo);
            if (validacion.EsFallo)
                return Result<EmpleadoAdministrativoDto>.Fallo(validacion.Error!);

            var existente = await _usuarioRepository.GetbyCorreo(dto.Correo ?? string.Empty);
            if (existente is not null)
                return Result<EmpleadoAdministrativoDto>.Fallo(DomainErrors.Usuarios.CorreoDuplicado);

            var existenteCodigo = await _usuarioRepository.GetByCodigoEmpleado(dto.CodigoEmpleado ?? string.Empty);
            if (existenteCodigo is not null)
                return Result<EmpleadoAdministrativoDto>.Fallo(DomainErrors.Usuarios.CodigoEmpleadoDuplicado);

            await _usuarioRepository.AddAsync(administrativo);
            await _auditoriaService.RegistrarAsync(administrativo.Id, "EmpleadoAdministrativoRegistrado", "Usuario", administrativo.Id.ToString(), $"Empleado administrativo registrado: {administrativo.Nombre} {administrativo.Apellido}");
            return Result<EmpleadoAdministrativoDto>.Ok(MapearEmpleadoAdministrativo(administrativo));
        }

        public async Task<Result<EmpleadoAdministrativoDto>> ActualizarEmpleadoAdministrativoAsync(int id, ActualizarEmpleadoAdministrativoDto dto)
        {
            var actual = await _usuarioRepository.GetByIdAsync(id);
            if (actual is not EmpleadoAdministrativoModel)
                return Result<EmpleadoAdministrativoDto>.Fallo(ApplicationErrors.NoEncontrado("el empleado administrativo"));

            var administrativo = new EmpleadoAdministrativo
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                CodigoEmpleado = dto.CodigoEmpleado,
                Departamento = dto.Departamento,
                Cargo = dto.Cargo,
                AreaAdministrativa = dto.AreaAdministrativa,
                TipoUsuario = "EmpleadoAdministrativo",
                Estado = actual.Estado,
                RolSistema = actual.RolSistema
            };

            var validacion = EmpleadoRules.Validar(administrativo);
            if (validacion.EsFallo)
                return Result<EmpleadoAdministrativoDto>.Fallo(validacion.Error!);

            var existente = await _usuarioRepository.GetbyCorreo(dto.Correo ?? string.Empty);
            if (existente is not null && existente.Id != id)
                return Result<EmpleadoAdministrativoDto>.Fallo(DomainErrors.Usuarios.CorreoDuplicado);

            var existenteCodigo = await _usuarioRepository.GetByCodigoEmpleado(dto.CodigoEmpleado ?? string.Empty);
            if (existenteCodigo is not null && existenteCodigo.Id != id)
                return Result<EmpleadoAdministrativoDto>.Fallo(DomainErrors.Usuarios.CodigoEmpleadoDuplicado);

            await _usuarioRepository.UpdateAsync(administrativo);
            return Result<EmpleadoAdministrativoDto>.Ok(MapearEmpleadoAdministrativo(administrativo));
        }

        public async Task<Result<ConductorDto>> RegistrarConductorAsync(CrearConductorDto dto)
        {
            var conductor = new Conductor
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                PasswordHash = SGA.Domain.Common.PasswordHasher.Hash(dto.PasswordHash ?? string.Empty),
                NumeroLicencia = dto.NumeroLicencia,
                FechaVencimientoLicencia = dto.FechaVencimientoLicencia,
                Disponible = true,
                TipoUsuario = "Conductor",
                RolSistema = SGA.Domain.Enum.RolUsuario.Conductor,
                CreadoPor = dto.CreadoPor
            };

            var validacion = ConductorRules.Validar(conductor);
            if (validacion.EsFallo)
                return Result<ConductorDto>.Fallo(validacion.Error!);

            var existente = await _usuarioRepository.GetbyCorreo(dto.Correo ?? string.Empty);
            if (existente is not null)
                return Result<ConductorDto>.Fallo(DomainErrors.Usuarios.CorreoDuplicado);

            var existenteLicencia = await _usuarioRepository.GetByNumeroLicencia(dto.NumeroLicencia ?? string.Empty);
            if (existenteLicencia is not null)
                return Result<ConductorDto>.Fallo(DomainErrors.CatalogoTransporte.LicenciaDuplicada);

            await _usuarioRepository.AddAsync(conductor);
            await _auditoriaService.RegistrarAsync(conductor.Id, "ConductorRegistrado", "Usuario", conductor.Id.ToString(), $"Conductor registrado: {conductor.Nombre} {conductor.Apellido}");
            return Result<ConductorDto>.Ok(MapearConductor(conductor));
        }

        public async Task<Result<ConductorDto>> ActualizarConductorAsync(int id, ActualizarConductorDto dto)
        {
            var actual = await _usuarioRepository.GetByIdAsync(id);
            if (actual is not ConductorModel conductorActual)
                return Result<ConductorDto>.Fallo(ApplicationErrors.NoEncontrado("el conductor"));

            var conductor = new Conductor
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                NumeroLicencia = dto.NumeroLicencia,
                FechaVencimientoLicencia = dto.FechaVencimientoLicencia,
                Disponible = conductorActual.Disponible,
                TipoUsuario = "Conductor",
                Estado = actual.Estado,
                RolSistema = actual.RolSistema
            };

            var validacion = ConductorRules.Validar(conductor);
            if (validacion.EsFallo)
                return Result<ConductorDto>.Fallo(validacion.Error!);

            var existente = await _usuarioRepository.GetbyCorreo(dto.Correo ?? string.Empty);
            if (existente is not null && existente.Id != id)
                return Result<ConductorDto>.Fallo(DomainErrors.Usuarios.CorreoDuplicado);

            var existenteLicencia = await _usuarioRepository.GetByNumeroLicencia(dto.NumeroLicencia ?? string.Empty);
            if (existenteLicencia is not null && existenteLicencia.Id != id)
                return Result<ConductorDto>.Fallo(DomainErrors.CatalogoTransporte.LicenciaDuplicada);

            await _usuarioRepository.UpdateAsync(conductor);
            return Result<ConductorDto>.Ok(MapearConductor(conductor));
        }

        public async Task<Result<ConductorDto>> CambiarDisponibilidadAsync(int id, CambiarDisponibilidadConductorDto dto)
        {
            var actual = await _usuarioRepository.GetByIdAsync(id);
            if (actual is not ConductorModel conductorActual)
                return Result<ConductorDto>.Fallo(ApplicationErrors.NoEncontrado("el conductor"));

            var conductor = new Conductor
            {
                Id = id,
                Nombre = conductorActual.Nombre,
                Apellido = conductorActual.Apellido,
                Correo = conductorActual.Correo,
                Telefono = conductorActual.Telefono,
                NumeroLicencia = conductorActual.NumeroLicencia,
                FechaVencimientoLicencia = conductorActual.FechaVencimientoLicencia,
                Disponible = dto.Disponible,
                TipoUsuario = "Conductor",
                Estado = conductorActual.Estado,
                RolSistema = conductorActual.RolSistema
            };

            await _usuarioRepository.UpdateAsync(conductor);
            return Result<ConductorDto>.Ok(MapearConductor(conductor));
        }

        public async Task<Result> EliminarAsync(int id, EliminarDto dto)
        {
            var actual = await _usuarioRepository.GetByIdAsync(id);
            if (actual is null)
                return Result.Fallo(ApplicationErrors.NoEncontrado("el usuario"));

            var entity = ConvertirEntidadVacia(actual, id);
            entity.Eliminado = true;
            entity.FechaEliminacion = DateTime.UtcNow;
            entity.EliminadoPor = dto.EliminadoPor;

            await _usuarioRepository.DeleteAsync(entity);
            await _auditoriaService.RegistrarAsync(id, "UsuarioEliminado", "Usuario", id.ToString(), $"Usuario dado de baja por {dto.EliminadoPor}. Motivo: {dto.Motivo}");
            return Result.Ok();
        }

        private static UsuarioTransporte ConvertirEntidadVacia(UsuarioModel m, int id) => m switch
        {
            EstudianteModel => new Estudiante { Id = id },
            ConductorModel => new Conductor { Id = id },
            EmpleadoDocenteModel => new EmpleadoDocente { Id = id },
            EmpleadoAdministrativoModel => new EmpleadoAdministrativo { Id = id },
            EmpleadoModel => new Empleado { Id = id },
            _ => new Estudiante { Id = id }
        };

        private static UsuarioResumenDto MapearResumen(UsuarioModel m) => new(
            m.Id, m.Nombre, m.Apellido, m.Correo, m.Telefono, m.Estado,
            m switch
            {
                EstudianteModel => "Estudiante",
                ConductorModel => "Conductor",
                EmpleadoDocenteModel => "EmpleadoDocente",
                EmpleadoAdministrativoModel => "EmpleadoAdministrativo",
                EmpleadoModel => "Empleado",
                AdministradorTransporteModel => "AdministradorTransporte",
                _ => m.GetType().Name
            },
            m.RolSistema);

        private static EstudianteDto MapearEstudiante(Estudiante e) =>
            new(e.Id, e.Nombre, e.Apellido, e.Correo, e.Telefono, e.Estado, e.RolSistema, e.Matricula, e.Carrera);

        private static EmpleadoDocenteDto MapearEmpleadoDocente(EmpleadoDocente e) =>
            new(e.Id, e.Nombre, e.Apellido, e.Correo, e.Telefono, e.Estado, e.CodigoEmpleado, e.Departamento, e.Cargo, e.Especialidad, e.TipoContrato);

        private static EmpleadoAdministrativoDto MapearEmpleadoAdministrativo(EmpleadoAdministrativo e) =>
            new(e.Id, e.Nombre, e.Apellido, e.Correo, e.Telefono, e.Estado, e.CodigoEmpleado, e.Departamento, e.Cargo, e.AreaAdministrativa);

        private static ConductorDto MapearConductor(Conductor c) =>
            new(c.Id, c.Nombre, c.Apellido, c.Correo, c.Telefono, c.Estado, c.NumeroLicencia, c.FechaVencimientoLicencia, c.Disponible);

        private static ConductorDto MapearConductor(ConductorModel c) =>
            new(c.Id, c.Nombre, c.Apellido, c.Correo, c.Telefono, c.Estado, c.NumeroLicencia, c.FechaVencimientoLicencia, c.Disponible);

        private static AdministradorTransporteDto MapearAdmin(AdministradorTransporte a) =>
            new(a.Id, a.Nombre, a.Apellido, a.Correo, a.Telefono, a.Estado, a.Departamento, a.Cargo);

        private static AdministradorTransporteDto MapearAdmin(AdministradorTransporteModel a) =>
            new(a.Id, a.Nombre, a.Apellido, a.Correo, a.Telefono, a.Estado, a.Departamento, a.Cargo);

        // ─── Administrador de Transporte ─────────────────────────────────────
        public async Task<Result<AdministradorTransporteDto>> RegistrarAdministradorTransporteAsync(CrearAdministradorTransporteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Correo))
                return Result<AdministradorTransporteDto>.Fallo(DomainErrors.General.CampoRequerido("Nombre y correo"));

            var existente = await _usuarioRepository.GetbyCorreo(dto.Correo ?? string.Empty);
            if (existente is not null)
                return Result<AdministradorTransporteDto>.Fallo(DomainErrors.Usuarios.CorreoDuplicado);

            var admin = new AdministradorTransporte
            {
                Nombre = dto.Nombre?.Trim(),
                Apellido = dto.Apellido?.Trim(),
                Correo = dto.Correo?.Trim().ToLower(),
                Telefono = dto.Telefono?.Trim(),
                PasswordHash = SGA.Domain.Common.PasswordHasher.Hash(dto.PasswordHash ?? string.Empty),
                Departamento = dto.Departamento?.Trim(),
                Cargo = dto.Cargo?.Trim(),
                TipoUsuario = "AdministradorTransporte",
                RolSistema = SGA.Domain.Enum.RolUsuario.AdministradorTransporte,
                Estado = "Activo",
                CreadoPor = dto.CreadoPor
            };

            await _usuarioRepository.AddAsync(admin);
            await _auditoriaService.RegistrarAsync(
                admin.Id, "AdminTransporteRegistrado", "Usuario", admin.Id.ToString(),
                $"Administrador de Transporte registrado: {admin.Nombre} {admin.Apellido} ({admin.Correo})");

            return Result<AdministradorTransporteDto>.Ok(MapearAdmin(admin));
        }

        public async Task<Result<AdministradorTransporteDto>> ActualizarAdministradorTransporteAsync(int id, ActualizarAdministradorTransporteDto dto)
        {
            var actual = await _usuarioRepository.GetByIdAsync(id);
            if (actual is not AdministradorTransporteModel)
                return Result<AdministradorTransporteDto>.Fallo(ApplicationErrors.NoEncontrado("el administrador"));

            var existente = await _usuarioRepository.GetbyCorreo(dto.Correo ?? string.Empty);
            if (existente is not null && existente.Id != id)
                return Result<AdministradorTransporteDto>.Fallo(DomainErrors.Usuarios.CorreoDuplicado);

            var admin = new AdministradorTransporte
            {
                Id = id,
                Nombre = dto.Nombre?.Trim(),
                Apellido = dto.Apellido?.Trim(),
                Correo = dto.Correo?.Trim().ToLower(),
                Telefono = dto.Telefono?.Trim(),
                Departamento = dto.Departamento?.Trim(),
                Cargo = dto.Cargo?.Trim(),
                TipoUsuario = "AdministradorTransporte",
                RolSistema = SGA.Domain.Enum.RolUsuario.AdministradorTransporte,
                Estado = actual.Estado
            };

            await _usuarioRepository.UpdateAsync(admin);
            return Result<AdministradorTransporteDto>.Ok(MapearAdmin(admin));
        }

        public async Task<Result<AdministradorTransporteDto>> ObtenerAdministradorPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario is not AdministradorTransporteModel admin
                ? Result<AdministradorTransporteDto>.Fallo(ApplicationErrors.NoEncontrado("el administrador"))
                : Result<AdministradorTransporteDto>.Ok(MapearAdmin(admin));
        }

        public async Task<Result<IReadOnlyList<AdministradorTransporteDto>>> ListarAdministradoresAsync()
        {
            var todos = await _usuarioRepository.GetAllAsync();
            var admins = todos.OfType<AdministradorTransporteModel>().Select(MapearAdmin).ToList();
            return Result<IReadOnlyList<AdministradorTransporteDto>>.Ok(admins);
        }
    }
}