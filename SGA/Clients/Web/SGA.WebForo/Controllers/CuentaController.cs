using Microsoft.AspNetCore.Mvc;
using SGA.Application.DTOs.Auth;
using SGA.Application.Interfaces.Services;

namespace SGA.WebForo.Controllers
{
    public class CuentaController : Controller
    {
        private readonly IAuthService _authService;

        public CuentaController(IAuthService authService)
        {
            _authService = authService;
        }

        // Login
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto credenciales, string? returnUrl)
        {
            var resultado = await _authService.IniciarSesionAsync(credenciales);

            if (resultado.EsFallo)
            {
                ViewBag.Error = resultado.Error!.Mensaje;
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            var sesion = resultado.Valor!;
            HttpContext.Session.SetInt32("UsuarioId", sesion.Id);
            HttpContext.Session.SetString("UsuarioNombre", $"{sesion.Nombre} {sesion.Apellido}".Trim());
            HttpContext.Session.SetString("TipoUsuario", sesion.TipoUsuario);

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Autobus");
        }

        //Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}