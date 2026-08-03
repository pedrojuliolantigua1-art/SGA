using SGA.Application.Common;
using SGA.Application.DTOs.Auth;
using SGA.Application.Interfaces.Services;
using SGA.Domain.Error;
using SGA.Domain.Models.Usuarios;
using SGA.Domain.Repository.Interfaces;

namespace SGA.Application.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
            => _usuarioRepository = usuarioRepository;

        public async Task<Result<SesionDto>> IniciarSesionAsync(LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Correo) || string.IsNullOrWhiteSpace(dto.Password))
                return Result<SesionDto>.Fallo(ApplicationErrors.CredencialesInvalidas);

            var usuario = await _usuarioRepository.GetbyCorreo(dto.Correo);
            if (usuario is null)
                return Result<SesionDto>.Fallo(ApplicationErrors.CredencialesInvalidas);

            if (!string.Equals(usuario.Estado, "Activo", StringComparison.OrdinalIgnoreCase))
                return Result<SesionDto>.Fallo(ApplicationErrors.UsuarioInactivo);

            var hashGuardado = await _usuarioRepository.ObtenerPasswordHashPorCorreoAsync(dto.Correo);
            if (!SGA.Domain.Common.PasswordHasher.Verificar(dto.Password, hashGuardado))
                return Result<SesionDto>.Fallo(ApplicationErrors.CredencialesInvalidas);

            return Result<SesionDto>.Ok(MapearSesion(usuario));
        }

        private static SesionDto MapearSesion(UsuarioModel u) => new(
            u.Id, u.Nombre, u.Apellido, u.Correo, u.RolSistema,
            u switch
            {
                EstudianteModel              => "Estudiante",
                ConductorModel               => "Conductor",
                EmpleadoDocenteModel         => "EmpleadoDocente",
                EmpleadoAdministrativoModel  => "EmpleadoAdministrativo",
                EmpleadoModel                => "Empleado",
                AdministradorTransporteModel => "AdministradorTransporte",
                _                            => u.GetType().Name
            });
    }
}
