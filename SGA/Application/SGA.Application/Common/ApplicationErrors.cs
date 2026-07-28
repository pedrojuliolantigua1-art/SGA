using SGA.Domain.Error;

namespace SGA.Application.Common
{
    public static class ApplicationErrors
    {
        public static Error NoEncontrado(string entidad) =>
            new("Aplicacion.NoEncontrado", $"No se encontro {entidad}.");

        public static Error OperacionInvalida(string mensaje) =>
            new("Aplicacion.OperacionInvalida", mensaje);

        public static readonly Error CredencialesInvalidas =
            new("Aplicacion.CredencialesInvalidas", "El correo o la contrasena son incorrectos.");

        public static readonly Error UsuarioInactivo =
            new("Aplicacion.UsuarioInactivo", "El usuario no se encuentra activo.");
    }

}
