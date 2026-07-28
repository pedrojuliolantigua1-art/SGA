using Microsoft.AspNetCore.Mvc;
using SGA.Application.DTOs.Pagos;
using SGA.Application.Interfaces.Services;

namespace SGA.WebForo.Controllers
{
    public class PagoController : Controller
    {
        private readonly IPagoService _pagoService;
        private readonly IUsuarioService _usuarioService;

        public PagoController(IPagoService pagoService, IUsuarioService usuarioService)
        {
            _pagoService = pagoService;
            _usuarioService = usuarioService;
        }

        // GET: PagoController
        public async Task<IActionResult> Index(DateTime? desde, DateTime? hasta)
        {
            var fechaDesde = desde ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var fechaHasta = hasta ?? DateTime.Today;

            var pagos = await _pagoService.ListarPorPeriodoAsync(fechaDesde, fechaHasta);

            if (pagos.EsFallo)
            {
                ViewBag.Error = pagos.Error!.Mensaje;
                ViewBag.Desde = fechaDesde;
                ViewBag.Hasta = fechaHasta;
                return View(new List<PagoDto>());
            }

            ViewBag.Desde = fechaDesde;
            ViewBag.Hasta = fechaHasta;
            return View(pagos.Valor!.ToList());
        }

        // GET: PagoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pago = await _pagoService.ObtenerPorIdAsync(id);

            if (pago.EsFallo)
            {
                ViewBag.Error = pago.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            return View(pago.Valor);
        }

        // GET: PagoController/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") is null)
                return RedirectToAction("Login", "Cuenta", new { returnUrl = Url.Action(nameof(Create), "Pago") });

            return View();
        }

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            string matricula,
            decimal monto,
            string tipoPago,
            string numeroComprobante,
            DateTime fechaHora)
        {
            var registradoPorId = HttpContext.Session.GetInt32("UsuarioId");

            if (registradoPorId is null)
                return RedirectToAction("Login", "Cuenta",
                    new { returnUrl = Url.Action(nameof(Create), "Pago") });

            var estudiante = await _usuarioService.ObtenerEstudiantePorMatriculaAsync(matricula);

            if (estudiante.EsFallo)
            {
                ViewBag.Error = $"No se encontró un estudiante con la matrícula '{matricula}'.";
                return View();
            }

            var dto = new RegistrarPagoDto(
                estudiante.Valor!.Id,
                monto,
                tipoPago,
                numeroComprobante,
                fechaHora,
                registradoPorId.Value,
                HttpContext.Session.GetString("UsuarioNombre")
            );

            var resultado = await _pagoService.RegistrarAsync(dto);

            if (resultado.EsFallo)
            {
                ViewBag.Error = resultado.Error!.Mensaje;
                return View();
            }

            return RedirectToAction(nameof(Details), new { id = resultado.Valor!.Id });
        }
    }
}