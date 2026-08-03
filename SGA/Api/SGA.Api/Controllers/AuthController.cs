using Microsoft.AspNetCore.Mvc;
using SGA.Api.Common;
using SGA.Application.DTOs.Auth;
using SGA.Application.Interfaces.Services;

namespace SGA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var resultado = await _authService.IniciarSesionAsync(dto);
            if (!resultado.EsExitoso)
                return this.AProblema(resultado.Error!);

            var sesion = resultado.Valor!;
            var token = _jwtService.GenerarToken(sesion);
            return Ok(new LoginResponseDto(token, "Bearer", sesion));
        }
    }
}
