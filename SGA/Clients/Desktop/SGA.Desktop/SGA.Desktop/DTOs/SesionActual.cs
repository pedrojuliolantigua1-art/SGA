using SGA.Desktop.Api;

namespace SGA.Desktop.Services
{
    public sealed class SesionDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public int RolSistema { get; set; }
        public string TipoUsuario { get; set; } = string.Empty;
    }

    public sealed class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string TipoToken { get; set; } = "Bearer";
        public SesionDto? Usuario { get; set; }
    }

    public static class SesionActual
    {
        public static SesionDto? Usuario { get; private set; }
        public static string? Token { get; private set; }

        public static void IniciarSesion(SesionDto usuario, string token)
        {
            Usuario = usuario;
            Token = token;
            SgaApiClient.EstablecerToken(token);
        }

        public static void CerrarSesion()
        {
            Usuario = null;
            Token = null;
            SgaApiClient.LimpiarToken();
        }

        public static bool EstaAutenticado => Usuario is not null;
    }
}
