using System.Net.Http.Json;
using System.Text.Json;
using SGA.Web.Models.Common;

namespace SGA.Web.Services
{
    /// <summary>
    /// Envoltorio simple sobre HttpClient para hablar con SGA.Api.
    /// Un único HttpClient inyectado (ver Program.cs) — nada de IHttpClientFactory / clientes tipados,
    /// tal como se pidió para esta entrega.
    /// </summary>
    public sealed class SgaApiService
    {
        private readonly HttpClient _http;
        private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web);

        public SgaApiService(HttpClient http) => _http = http;

        /// <summary>Establece el token JWT en el header Authorization para todas las peticiones futuras.</summary>
        public void EstablecerToken(string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>Elimina el header Authorization al cerrar sesión.</summary>
        public void LimpiarToken()
        {
            _http.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<ApiResult<T>> GetAsync<T>(string ruta)
        {
            try
            {
                var respuesta = await _http.GetAsync(ruta);
                return await LeerRespuestaAsync<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        public async Task<ApiResult<T>> PostAsync<T>(string ruta, object body)
        {
            try
            {
                var respuesta = await _http.PostAsJsonAsync(ruta, body, JsonOpts);
                return await LeerRespuestaAsync<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        public async Task<ApiResult<T>> PostContentAsync<T>(string ruta, HttpContent contenido)
        {
            try
            {
                var respuesta = await _http.PostAsync(ruta, contenido);
                return await LeerRespuestaAsync<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        public async Task<ApiResult<T>> PutAsync<T>(string ruta, object body)
        {
            try
            {
                var respuesta = await _http.PutAsJsonAsync(ruta, body, JsonOpts);
                return await LeerRespuestaAsync<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        public async Task<ApiResult<T>> DeleteAsync<T>(string ruta)
        {
            try
            {
                var respuesta = await _http.DeleteAsync(ruta);
                return await LeerRespuestaAsync<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        public string ObtenerUrlAbsoluta(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return string.Empty;

            if (Uri.TryCreate(url, UriKind.Absolute, out _))
                return url;

            if (_http.BaseAddress is null)
                return url;

            return new Uri(_http.BaseAddress, url.TrimStart('/')).ToString();
        }

        private static async Task<ApiResult<T>> LeerRespuestaAsync<T>(HttpResponseMessage respuesta)
        {
            if (respuesta.IsSuccessStatusCode)
            {
                if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return ApiResult<T>.Ok(default);

                var texto = await respuesta.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(texto))
                    return ApiResult<T>.Ok(default);

                var valor = JsonSerializer.Deserialize<T>(texto, JsonOpts);
                return ApiResult<T>.Ok(valor);
            }

            var mensaje = "Ocurrió un error inesperado.";
            try
            {
                var problema = await respuesta.Content.ReadFromJsonAsync<ProblemaApi>(JsonOpts);
                if (problema?.Detail is not null) mensaje = problema.Detail;
                else if (problema?.Title is not null) mensaje = problema.Title;
            }
            catch { /* el cuerpo no era JSON de problema; usamos el mensaje genérico */ }

            return ApiResult<T>.Fallo(mensaje);
        }

        private sealed class ProblemaApi
        {
            public string? Title { get; set; }
            public string? Detail { get; set; }
        }
    }
}
