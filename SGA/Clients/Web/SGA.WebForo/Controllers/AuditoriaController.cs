using Microsoft.AspNetCore.Mvc;
using SGA.Application.Interfaces.Services;

namespace SGA.WebForo.Controllers
{
    // Modulo de solo lectura: los registros de auditoria se generan automaticamente
    public class AuditoriaController : Controller
    {
        private readonly IAuditoriaService _auditoriaService;

        public AuditoriaController(IAuditoriaService auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        // GET: AuditoriaController
        public async Task<IActionResult> Index(DateTime? desde, DateTime? hasta, int? actorId, string? accion)
        {
            var fechaDesde = desde ?? DateTime.Today.AddDays(-30);
            var fechaHasta = hasta ?? DateTime.Today;

            var resultado = actorId is not null
                ? await _auditoriaService.ListarPorActorAsync(actorId.Value)
                : !string.IsNullOrWhiteSpace(accion)
                    ? await _auditoriaService.ListarPorAccionAsync(accion)
                    : await _auditoriaService.ListarPorPeriodoAsync(fechaDesde, fechaHasta);

            ViewBag.Desde = fechaDesde;
            ViewBag.Hasta = fechaHasta;
            ViewBag.ActorId = actorId;
            ViewBag.Accion = accion;

            if (resultado.EsFallo)
            {
                ViewBag.Error = resultado.Error!.Mensaje;
                return View(new List<SGA.Application.DTOs.Auditoria.AuditoriaDto>());
            }

            return View(resultado.Valor!.OrderByDescending(r => r.FechaHora).ToList());
        }

        // GET: AuditoriaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var registro = await _auditoriaService.ObtenerPorIdAsync(id);

            if (registro.EsFallo)
            {
                ViewBag.Error = registro.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            return View(registro.Valor);
        }
    }
}
