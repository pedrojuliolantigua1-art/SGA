namespace SGA.Web.Models.Auth
{
    public sealed class LoginModel
    {
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>Refleja SesionDto de la API. RolSistema llega como número (no hay JsonStringEnumConverter en la API).</summary>
    public sealed class SesionModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public int RolSistema { get; set; }
        public string TipoUsuario { get; set; } = string.Empty;

        public string NombreCompleto => $"{Nombre} {Apellido}".Trim();

        public bool EsConductor => TipoUsuario.Equals("Conductor", StringComparison.OrdinalIgnoreCase);
        public bool EsEstudiante => TipoUsuario.Equals("Estudiante", StringComparison.OrdinalIgnoreCase);
        public bool EsEmpleado => TipoUsuario.Equals("EmpleadoDocente", StringComparison.OrdinalIgnoreCase)
                                  || TipoUsuario.Equals("EmpleadoAdministrativo", StringComparison.OrdinalIgnoreCase);
        public bool EsAdministradorTransporte => RolSistema == 1
                                  || TipoUsuario.Equals("AdministradorTransporte", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>Respuesta completa de POST /api/auth/login — token JWT + datos de sesión.</summary>
    public sealed class LoginResponseModel
    {
        public string Token { get; set; } = string.Empty;
        public string TipoToken { get; set; } = "Bearer";
        public SesionModel? Usuario { get; set; }
    }
}
