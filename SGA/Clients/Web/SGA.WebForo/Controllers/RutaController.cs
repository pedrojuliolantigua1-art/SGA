using Microsoft.AspNetCore.Mvc;
using SGA.Application.DTOs.Common;
using SGA.Application.DTOs.Horarios;
using SGA.Application.DTOs.Paradas;
using SGA.Application.DTOs.Rutas;
using SGA.Application.Interfaces.Services;

namespace SGA.WebForo.Controllers
{
    public class RutaController : Controller
    {
        private readonly IRutaService _rutaService;
        private readonly IParadaService _paradaService;
        private readonly IHorarioRutaService _horarioRutaService;

        public RutaController(
            IRutaService rutaService, IParadaService paradaService, IHorarioRutaService horarioRutaService)
        {
            _rutaService = rutaService;
            _paradaService = paradaService;
            _horarioRutaService = horarioRutaService;
        }

        // GET: RutaController
        public async Task<IActionResult> Index(string? nombre)
        {
            var rutas = await _rutaService.ListarTodasAsync();

            if (rutas.EsFallo)
            {
                ViewBag.Error = rutas.Error!.Mensaje;
                return View(new List<RutaDto>());
            }

            IEnumerable<RutaDto> resultado = rutas.Valor!;

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                resultado = resultado.Where(r =>
                    r.Nombre != null && r.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
            }

            ViewBag.Nombre = nombre;
            return View(resultado.ToList());
        }

        // GET: RutaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var detalle = await _rutaService.ObtenerDetalleAsync(id);

            if (detalle.EsFallo)
            {
                ViewBag.Error = detalle.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            return View(detalle.Valor);
        }

        // GET: RutaController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RutaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearRutaDto ruta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(ruta);
                }

                var resultado = await _rutaService.CrearAsync(ruta);

                if (resultado.EsFallo)
                {
                    ViewBag.Error = resultado.Error!.Mensaje;
                    return View(ruta);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RutaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ruta = await _rutaService.ObtenerPorIdAsync(id);

            if (ruta.EsFallo)
            {
                ViewBag.Error = ruta.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            var dto = ruta.Valor!;
            var editar = new ActualizarRutaDto(dto.Nombre, dto.Descripcion, dto.Activa);

            ViewBag.Id = id;
            return View(editar);
        }

        // POST: RutaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActualizarRutaDto ruta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Id = id;
                    return View(ruta);
                }

                var resultado = await _rutaService.ActualizarAsync(id, ruta);

                if (resultado.EsFallo)
                {
                    ViewBag.Error = resultado.Error!.Mensaje;
                    ViewBag.Id = id;
                    return View(ruta);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RutaController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ruta = await _rutaService.ObtenerPorIdAsync(id);

            if (ruta.EsFallo)
            {
                ViewBag.Error = ruta.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            return View(ruta.Valor);
        }

        // POST: RutaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, EliminarDto ruta)
        {
            var resultado = await _rutaService.EliminarAsync(id, ruta);

            if (resultado.EsFallo)
                ViewBag.Error = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Index));
        }

        // AgregarParada
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarParada(int rutaId, string nombre, string? referencia, int orden)
        {
            var resultado = await _paradaService.CrearAsync(
                new CrearParadaDto(rutaId, nombre, referencia, orden, User.Identity?.Name));

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Details), new { id = rutaId });
        }

        // POST: EliminarParada
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarParada(int paradaId, int rutaId)
        {
            var resultado = await _paradaService.EliminarAsync(
                paradaId, new EliminarDto(null, User.Identity?.Name));

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Details), new { id = rutaId });
        }

        // AgregarHorario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarHorario(int rutaId, TimeSpan horaSalida, TimeSpan horaLlegadaEstimada)
        {
            var resultado = await _horarioRutaService.CrearAsync(
                new CrearHorarioRutaDto(rutaId, horaSalida, horaLlegadaEstimada, User.Identity?.Name));

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Details), new { id = rutaId });
        }

        // POST: EliminarHorario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarHorario(int horarioId, int rutaId)
        {
            var resultado = await _horarioRutaService.EliminarAsync(
                horarioId, new EliminarDto(null, User.Identity?.Name));

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Details), new { id = rutaId });
        }
    }
}
