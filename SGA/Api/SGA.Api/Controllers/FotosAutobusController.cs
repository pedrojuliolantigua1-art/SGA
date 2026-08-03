using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGA.Api.Common;
using SGA.Application.Interfaces.Services;

namespace SGA.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public sealed class FotosAutobusController : ControllerBase
    {
        private const long MaxBytes = 5 * 1024 * 1024;

        private readonly IFotoAutobusService _fotoService;
        private readonly ICurrentUserService _currentUser;

        public FotosAutobusController(IFotoAutobusService fotoService, ICurrentUserService currentUser)
        {
            _fotoService = fotoService;
            _currentUser = currentUser;
        }

        [HttpGet("autobus/{autobusId:int}")]
        public async Task<IActionResult> ListarPorAutobus(int autobusId)
            => this.AResultado(await _fotoService.ListarPorAutobusAsync(autobusId));

        [HttpPost]
        public async Task<IActionResult> Subir([FromForm] int autobusId, IFormFile archivo)
        {
            if (archivo is null)
                return BadRequest(new { detail = "El archivo de imagen es obligatorio." });

            if (archivo.Length <= 0)
                return BadRequest(new { detail = "El archivo esta vacio." });

            if (archivo.Length > MaxBytes)
                return BadRequest(new { detail = "La foto no puede superar los 5 MB." });

            using var memoria = new MemoryStream();
            await archivo.CopyToAsync(memoria);

            var resultado = await _fotoService.SubirAsync(
                autobusId, memoria.ToArray(), archivo.FileName, _currentUser.UsuarioId.ToString());

            return this.AResultadoCreado(resultado);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
            => this.AResultado(await _fotoService.EliminarAsync(id));
    }
}