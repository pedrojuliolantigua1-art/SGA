using Microsoft.AspNetCore.Mvc;
using SGA.Application.DTOs.Common;
using SGA.Application.DTOs.Usuarios;
using SGA.Application.Interfaces.Services;

namespace SGA.WebForo.Controllers
{
    public class ConductorController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public ConductorController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: ConductorController
        public async Task<IActionResult> Index(string? nombre)
        {
            var conductores = await _usuarioService.ListarConductoresAsync();

            if (conductores.EsFallo)
            {
                ViewBag.Error = conductores.Error!.Mensaje;
                return View(new List<ConductorDto>());
            }

            IEnumerable<ConductorDto> resultado = conductores.Valor!;

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                resultado = resultado.Where(c =>
                    (c.Nombre != null && c.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Apellido != null && c.Apellido.Contains(nombre, StringComparison.OrdinalIgnoreCase)));
            }

            ViewBag.Nombre = nombre;
            return View(resultado.ToList());
        }

        // GET: ConductorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var conductor = await _usuarioService.ObtenerConductorPorIdAsync(id);

            if (conductor.EsFallo)
            {
                ViewBag.Error = conductor.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            return View(conductor.Valor);
        }

        // GET: ConductorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConductorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearConductorDto conductor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(conductor);
                }

                var resultado = await _usuarioService.RegistrarConductorAsync(conductor);

                if (resultado.EsFallo)
                {
                    ViewBag.Error = resultado.Error!.Mensaje;
                    return View(conductor);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConductorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var conductor = await _usuarioService.ObtenerConductorPorIdAsync(id);

            if (conductor.EsFallo)
            {
                ViewBag.Error = conductor.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            var dto = conductor.Valor!;
            var editar = new ActualizarConductorDto(
                dto.Nombre, dto.Apellido, dto.Correo, dto.Telefono, dto.NumeroLicencia, dto.FechaVencimientoLicencia);

            ViewBag.Id = id;
            return View(editar);
        }

        // POST: ConductorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActualizarConductorDto conductor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Id = id;
                    return View(conductor);
                }

                var resultado = await _usuarioService.ActualizarConductorAsync(id, conductor);

                if (resultado.EsFallo)
                {
                    ViewBag.Error = resultado.Error!.Mensaje;
                    ViewBag.Id = id;
                    return View(conductor);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConductorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var conductor = await _usuarioService.ObtenerConductorPorIdAsync(id);

            if (conductor.EsFallo)
            {
                ViewBag.Error = conductor.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            return View(conductor.Valor);
        }

        // POST: ConductorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, EliminarDto conductor)
        {
            try
            {
                var resultado = await _usuarioService.EliminarAsync(id, conductor);

                if (resultado.EsFallo)
                {
                    ViewBag.Error = resultado.Error!.Mensaje;
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //CambiarDisponibilidad
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarDisponibilidad(int id, bool disponible)
        {
            var resultado = await _usuarioService.CambiarDisponibilidadAsync(
                id, new CambiarDisponibilidadConductorDto(disponible));

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
