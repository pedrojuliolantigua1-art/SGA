using System;
using System.Collections.Generic;
using System.Text;

namespace SGA.Desktop.Api
{
    public sealed class ApiResult<T>
    {
        public bool EsExitoso { get; init; }
        public T? Valor { get; init; }
        public string? Error { get; init; }

        public static ApiResult<T> Ok(T valor) => new() { EsExitoso = true, Valor = valor };
        public static ApiResult<T> Fallo(string error) => new() { EsExitoso = false, Error = error };
    }
}
