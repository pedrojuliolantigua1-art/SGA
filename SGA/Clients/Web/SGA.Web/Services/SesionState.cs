using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Microsoft.JSInterop;
using SGA.Web.Models.Auth;

namespace SGA.Web.Services
{
    /// <summary>Sesión del usuario actual. Persiste en localStorage para sobrevivir un refresco de página.</summary>
    public sealed class SesionState
    {
        private const string ClaveAlmacenamiento = "sga.sesion";
        private const string ClaveToken = "sga.token";
        private readonly IJSRuntime _js;
        private readonly SgaApiService _api;

        public SesionModel? Usuario { get; private set; }
        public string? Token { get; private set; }
        public bool EstaAutenticado => Usuario is not null;

        public event Action? CambioSesion;

        public SesionState(IJSRuntime js, SgaApiService api)
        {
            _js = js;
            _api = api;
        }

        private bool TokenExpirado(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
                return true;

            var jwt = handler.ReadJwtToken(token);

            return jwt.ValidTo <= DateTime.UtcNow;
        }

        public async Task IniciarSesionAsync(SesionModel usuario, string token)
        {
            Usuario = usuario;
            Token = token;
            _api.EstablecerToken(token);

            var json = JsonSerializer.Serialize(usuario);
            await _js.InvokeVoidAsync("localStorage.setItem", ClaveAlmacenamiento, json);
            await _js.InvokeVoidAsync("localStorage.setItem", ClaveToken, token);
            CambioSesion?.Invoke();
        }

        public async Task CerrarSesionAsync()
        {
            Usuario = null;
            Token = null;
            _api.LimpiarToken();
            await _js.InvokeVoidAsync("localStorage.removeItem", ClaveAlmacenamiento);
            await _js.InvokeVoidAsync("localStorage.removeItem", ClaveToken);
            CambioSesion?.Invoke();
        }

        /// <summary>Restaura la sesión y el token desde localStorage (llamar una vez al iniciar la app / layout).</summary>
        public async Task RestaurarAsync()
        {
            if (Usuario is not null)
                return;

            var json = await _js.InvokeAsync<string?>("localStorage.getItem", ClaveAlmacenamiento);
            var token = await _js.InvokeAsync<string?>("localStorage.getItem", ClaveToken);

            if (string.IsNullOrWhiteSpace(json) || string.IsNullOrWhiteSpace(token))
                return;

            if (TokenExpirado(token))
            {
                await CerrarSesionAsync();
                return;
            }

            Usuario = JsonSerializer.Deserialize<SesionModel>(json);

            if (Usuario is null)
            {
                await CerrarSesionAsync();
                return;
            }

            Token = token;
            _api.EstablecerToken(token);

            CambioSesion?.Invoke();
        }
    }
}
