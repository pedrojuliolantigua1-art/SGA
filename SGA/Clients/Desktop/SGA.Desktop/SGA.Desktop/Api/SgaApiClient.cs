using System.Net.Http.Json;
using System.Text.Json;

namespace SGA.Desktop.Api
{

    public static class SgaApiClient
    {
        private static readonly HttpClient _http = new()
        {
            BaseAddress = new Uri("https://localhost:7168/"),
            
        };

        private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web);

        /// Configura el token JWT en el header Authorization de todas las peticiones futuras.
        public static void EstablecerToken(string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public static void LimpiarToken()
        {
            _http.DefaultRequestHeaders.Authorization = null;
        }

        public static async Task<ApiResult<T>> GetAsync<T>(string ruta)
        {
            try
            {
                var respuesta = await _http.GetAsync(ruta);
                return await LeerRespuesta<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        public static async Task<ApiResult<T>> PostAsync<T>(string ruta, object body)
        {
            try
            {
                var respuesta = await _http.PostAsJsonAsync(ruta, body, JsonOpts);
                return await LeerRespuesta<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        public static async Task<ApiResult<T>> PutAsync<T>(string ruta, object body)
        {
            try
            {
                var respuesta = await _http.PutAsJsonAsync(ruta, body, JsonOpts);
                return await LeerRespuesta<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        // Para endpoints [HttpPatch], como cambiar el estado del autobus.
        public static async Task<ApiResult<T>> PatchAsync<T>(string ruta, object body)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Patch, ruta)
                {
                    Content = JsonContent.Create(body, options: JsonOpts)
                };
                var respuesta = await _http.SendAsync(request);
                return await LeerRespuesta<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        public static async Task<ApiResult<T>> DeleteAsync<T>(string ruta, object body)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, ruta)
                {
                    Content = JsonContent.Create(body, options: JsonOpts)
                };
                var respuesta = await _http.SendAsync(request);
                return await LeerRespuesta<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        public static async Task<ApiResult<T>> PostContentAsync<T>(string ruta, HttpContent contenido)
        {
            try
            {
                var respuesta = await _http.PostAsync(ruta, contenido);
                return await LeerRespuesta<T>(respuesta);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Fallo($"No se pudo conectar con el servidor: {ex.Message}");
            }
        }

        private static async Task<ApiResult<T>> LeerRespuesta<T>(HttpResponseMessage respuesta)
        {
            if (respuesta.IsSuccessStatusCode)
            {
                if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return ApiResult<T>.Ok(default!);

                var texto = await respuesta.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(texto))
                    return ApiResult<T>.Ok(default!);

                var valor = JsonSerializer.Deserialize<T>(texto, JsonOpts);
                return ApiResult<T>.Ok(valor!);
            }

            string mensaje = "Ocurrió un error inesperado.";
            try
            {
                var problema = await respuesta.Content.ReadFromJsonAsync<ProblemaApi>(JsonOpts);
                if (problema?.Detail is not null) mensaje = problema.Detail;
                else if (problema?.Title is not null) mensaje = problema.Title;
            }
            catch {}

            return ApiResult<T>.Fallo(mensaje);
        }

        private sealed class ProblemaApi
        {
            public string? Title { get; set; }
            public string? Detail { get; set; }
        }
    }
}
