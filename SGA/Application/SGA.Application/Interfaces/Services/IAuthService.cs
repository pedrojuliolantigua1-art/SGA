using SGA.Application.DTOs.Auth;
using SGA.Domain.Error;

namespace SGA.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Result<SesionDto>> IniciarSesionAsync(LoginDto dto);
    }
}
