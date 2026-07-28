using Microsoft.AspNetCore.Mvc;
using SGA.Application.DTOs.Autobuses;
using SGA.Application.DTOs.Common;
using SGA.Application.DTOs.Fotos;
using SGA.Application.Interfaces.Services;

namespace SGA.WebForo.Controllers
{
    public class AutobusController : Controller
    {
        private readonly IAutobusService _autobusService;
        private readonly IFotoAutobusService _fotoAutobusService;

        public AutobusController(IAutobusService autobusService, IFotoAutobusService fotoAutobusService)
        {
            _autobusService = autobusService;
            _fotoAutobusService = fotoAutobusService;
        }
        // GET: AutobusController
        public async Task<IActionResult> Index(string? placa)
        {
            var autobuses = await _autobusService.ListarDisponiblesAsync();

            if (autobuses.EsFallo)
            {
                ViewBag.Error = autobuses.Error!.Mensaje;
                return View(new List<AutobusDto>());
            }

            IEnumerable<AutobusDto> resultado = autobuses.Valor!;

            if (!string.IsNullOrWhiteSpace(placa))
            {
                resultado = resultado.Where(a =>
                    a.Placa != null && a.Placa.Contains(placa, StringComparison.OrdinalIgnoreCase));
            }

            ViewBag.Placa = placa;
            return View(resultado.ToList());
        }

        // GET: AutobusController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var autobus = await _autobusService.ObtenerPorIdAsync(id);

            if (autobus.EsFallo)
            {
                ViewBag.Error = autobus.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            var fotos = await _fotoAutobusService.ListarPorAutobusAsync(id);
            ViewBag.Fotos = fotos.Valor?.ToList();

            return View(autobus.Valor);
        }

        // GET: AutobusController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutobusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearAutobusDto autobus, IFormFile? foto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(autobus);
                }

                var resultado = await _autobusService.CrearAsync(autobus);

                if (resultado.EsFallo)
                {
                    ViewBag.Error = resultado.Error!.Mensaje;
                    return View(autobus);
                }

                if (foto is not null && foto.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await foto.CopyToAsync(ms);
                    await _fotoAutobusService.SubirAsync(
                        resultado.Valor!.Id, ms.ToArray(), foto.FileName, User.Identity?.Name);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutobusController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var autobus = await _autobusService.ObtenerPorIdAsync(id);

            if (autobus.EsFallo)
            {
                ViewBag.Error = autobus.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            var dto = autobus.Valor!;
            var editar = new ActualizarAutobusDto(dto.Placa!, dto.Marca!, dto.Modelo!, dto.Capacidad);

            ViewBag.Id = id;

            var fotos = await _fotoAutobusService.ListarPorAutobusAsync(id);
            ViewBag.Fotos = fotos.Valor?.ToList();

            return View(editar);
        }

        // POST: AutobusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActualizarAutobusDto autobus, IFormFile? foto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Id = id;
                    return View(autobus);
                }

                var resultado = await _autobusService.ActualizarAsync(id, autobus);

                if (resultado.EsFallo)
                {
                    ViewBag.Error = resultado.Error!.Mensaje;
                    ViewBag.Id = id;
                    return View(autobus);
                }

                if (foto is not null && foto.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await foto.CopyToAsync(ms);
                    await _fotoAutobusService.SubirAsync(id, ms.ToArray(), foto.FileName, User.Identity?.Name);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: AutobusController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var autobus = await _autobusService.ObtenerPorIdAsync(id);

            if (autobus.EsFallo)
            {
                ViewBag.Error = autobus.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            return View(autobus.Valor);
        }

        // POST: AutobusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, EliminarDto autobus)
        {
            try
            {
                var resultado = await _autobusService.EliminarAsync(id, autobus);

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

        // GET: AutobusController/Fotos/5
        public async Task<IActionResult> Fotos(int id)
        {
            var autobus = await _autobusService.ObtenerPorIdAsync(id);
            if (autobus.EsFallo)
            {
                ViewBag.Error = autobus.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            var fotos = await _fotoAutobusService.ListarPorAutobusAsync(id);

            ViewBag.AutobusId = id;
            ViewBag.Placa = autobus.Valor!.Placa;
            return View(fotos.Valor ?? new List<FotoAutobusDto>());
        }

        // POST: AutobusController/SubirFoto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubirFoto(int autobusId, IFormFile archivo)
        {
            if (archivo is null || archivo.Length == 0)
            {
                TempData["Error"] = "Selecciona un archivo.";
                return RedirectToAction(nameof(Fotos), new { id = autobusId });
            }

            using var ms = new MemoryStream();
            await archivo.CopyToAsync(ms);

            var resultado = await _fotoAutobusService.SubirAsync(
                autobusId, ms.ToArray(), archivo.FileName, User.Identity?.Name);

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Fotos), new { id = autobusId });
        }

        // POST: AutobusController/EliminarFoto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarFoto(int fotoId, int autobusId)
        {
            var resultado = await _fotoAutobusService.EliminarAsync(fotoId);

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Fotos), new { id = autobusId });
        }
    }
}
