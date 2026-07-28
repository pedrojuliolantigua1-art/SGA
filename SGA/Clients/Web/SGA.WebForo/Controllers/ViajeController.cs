using Microsoft.AspNetCore.Mvc;
using SGA.Application.DTOs.Horarios;
using SGA.Application.DTOs.Viajes;
using SGA.Application.Interfaces.Services;

namespace SGA.WebForo.Controllers
{
    public class ViajeController : Controller
    {
        private readonly IViajeService _viajeService;
        private readonly IRutaService _rutaService;
        private readonly IHorarioRutaService _horarioRutaService;
        private readonly IAutobusService _autobusService;
        private readonly IUsuarioService _usuarioService;

        public ViajeController(
            IViajeService viajeService, IRutaService rutaService, IHorarioRutaService horarioRutaService,
            IAutobusService autobusService, IUsuarioService usuarioService)
        {
            _viajeService = viajeService;
            _rutaService = rutaService;
            _horarioRutaService = horarioRutaService;
            _autobusService = autobusService;
            _usuarioService = usuarioService;
        }

        // GET: ViajeController
        public async Task<IActionResult> Index(DateTime? fecha)
        {
            var fechaConsulta = fecha ?? DateTime.Today;
            var viajes = await _viajeService.ListarPorFechaAsync(fechaConsulta);

            if (viajes.EsFallo)
            {
                ViewBag.Error = viajes.Error!.Mensaje;
                ViewBag.Fecha = fechaConsulta;
                return View(new List<ViajeDto>());
            }

            ViewBag.Fecha = fechaConsulta;
            return View(viajes.Valor!.ToList());
        }

        // GET: ViajeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var viaje = await _viajeService.ObtenerPorIdAsync(id);

            if (viaje.EsFallo)
            {
                ViewBag.Error = viaje.Error!.Mensaje;
                return RedirectToAction(nameof(Index));
            }

            return View(viaje.Valor);
        }

        // GET: ViajeController/Create
        public async Task<IActionResult> Create()
        {
            await CargarListasAsync();
            return View();
        }

        // POST: ViajeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgramarViajeDto viaje)
        {
            try
            {
                var resultado = await _viajeService.ProgramarAsync(viaje with { CreadoPor = User.Identity?.Name });

                if (resultado.EsFallo)
                {
                    ViewBag.Error = resultado.Error!.Mensaje;
                    await CargarListasAsync();
                    return View(viaje);
                }

                return RedirectToAction(nameof(Index), new { fecha = viaje.Fecha });
            }
            catch
            {
                await CargarListasAsync();
                return View(viaje);
            }
        }

        //Iniciar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Iniciar(int viajeId, int conductorId)
        {
            var resultado = await _viajeService.IniciarAsync(new EjecutarViajeDto(viajeId, conductorId, DateTime.Now));

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Details), new { id = viajeId });
        }

        //Finalizar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finalizar(int viajeId, int conductorId)
        {
            var resultado = await _viajeService.FinalizarAsync(new EjecutarViajeDto(viajeId, conductorId, DateTime.Now));

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Details), new { id = viajeId });
        }

        //Cancelar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int viajeId, string motivo)
        {
            var resultado = await _viajeService.CancelarAsync(
                new CancelarViajeDto(viajeId, motivo, User.Identity?.Name));

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Details), new { id = viajeId });
        }

        //ReportarIncidencia
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportarIncidencia(int viajeId, int conductorId, string tipo, string descripcion)
        {
            var resultado = await _viajeService.ReportarIncidenciaAsync(
                new ReportarIncidenciaDto(viajeId, conductorId, tipo, descripcion, DateTime.Now, User.Identity?.Name));

            if (resultado.EsFallo)
                TempData["Error"] = resultado.Error!.Mensaje;

            return RedirectToAction(nameof(Details), new { id = viajeId });
        }

        private async Task CargarListasAsync()
        {
            var rutas = await _rutaService.ListarActivasAsync();
            var autobuses = await _autobusService.ListarDisponiblesAsync();
            var conductores = await _usuarioService.ListarConductoresAsync();

            ViewBag.Rutas = rutas.Valor?.ToList() ?? new();
            ViewBag.Autobuses = autobuses.Valor?.ToList() ?? new();
            ViewBag.Conductores = conductores.Valor?.Where(c => c.Disponible).ToList() ?? new();

            var horarios = new List<HorarioRutaDto>();
            foreach (var ruta in ViewBag.Rutas)
            {
                var horariosRuta = await _horarioRutaService.ListarPorRutaAsync(ruta.Id);
                if (horariosRuta.EsExitoso)
                    horarios.AddRange(horariosRuta.Valor!);
            }
            ViewBag.Horarios = horarios;
        }
    }
}
