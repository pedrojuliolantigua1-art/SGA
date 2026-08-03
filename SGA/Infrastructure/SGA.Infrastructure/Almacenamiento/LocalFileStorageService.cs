using SGA.Domain.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SGA.Infrastructure.Almacenamiento
{
    /// <summary>
    /// Guarda las imagenes en una carpeta local del servidor y en la base de datos
    /// solo se guarda el enlace (UrlPublica). La API las sirve por medio de UseStaticFiles.
    /// </summary>
    public sealed class LocalFileStorageService : IAlmacenamientoArchivos
    {
        private readonly string _rutaRaiz;
        private readonly string _urlBase;

        public LocalFileStorageService(string rutaRaiz, string urlBase = "/fotos-autobus")
        {
            _rutaRaiz = Path.GetFullPath(rutaRaiz);
            _urlBase = urlBase.TrimEnd('/');
            Directory.CreateDirectory(_rutaRaiz);
        }

        public Task<ResultadoSubida> SubirAsync(byte[] contenido, string nombreArchivo, string carpeta)
        {
            var subcarpeta = carpeta.Replace('\\', '/').Trim('/');
            if (subcarpeta.Length == 0)
                subcarpeta = "general";

            var directorio = Path.Combine(_rutaRaiz, subcarpeta);
            Directory.CreateDirectory(directorio);

            var extension = Path.GetExtension(nombreArchivo);
            var nombre = $"{Guid.NewGuid():N}{extension}";
            var rutaCompleta = Path.Combine(directorio, nombre);

            File.WriteAllBytes(rutaCompleta, contenido);

            var url = $"{_urlBase}/{subcarpeta}/{nombre}";
            var publicId = $"{subcarpeta}/{nombre}";

            return Task.FromResult(new ResultadoSubida(url, publicId, nombreArchivo));
        }

        public Task EliminarAsync(string publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId))
                return Task.CompletedTask;

            var relativo = publicId.Replace('/', Path.DirectorySeparatorChar)
                                   .TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var ruta = Path.GetFullPath(Path.Combine(_rutaRaiz, relativo));

            // Evita que un PublicId manipulado pueda salir de la carpeta raiz
            if (ruta.StartsWith(_rutaRaiz, StringComparison.OrdinalIgnoreCase) && File.Exists(ruta))
                File.Delete(ruta);

            return Task.CompletedTask;
        }
    }
}
