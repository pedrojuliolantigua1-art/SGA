namespace SGA.Web.Models.Common
{
    /// <summary>Envoltorio uniforme para el resultado de cualquier llamada a la API del SGA.</summary>
    public sealed class ApiResult<T>
    {
        public bool EsExitoso { get; init; }
        public T? Valor { get; init; }
        public string? Error { get; init; }

        public static ApiResult<T> Ok(T? valor) => new() { EsExitoso = true, Valor = valor };
        public static ApiResult<T> Fallo(string error) => new() { EsExitoso = false, Error = error };
    }
}
