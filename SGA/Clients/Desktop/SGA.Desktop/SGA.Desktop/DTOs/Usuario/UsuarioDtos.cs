namespace SGA.Desktop.DTOs.Usuario
{
    /// <summary>Vista resumida usada en el listado general de usuarios (todos los tipos).</summary>
    public sealed class UsuarioResumenPresentacionDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = string.Empty;

        // Llega como número desde la API (no hay JsonStringEnumConverter configurado).
        public int RolSistema { get; set; }
    }

    public sealed class EstudiantePresentacionDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Matricula { get; set; }
        public string? Carrera { get; set; }
    }

    public sealed class CrearEstudiantePresentacionDto
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? PasswordHash { get; set; }
        public string? Matricula { get; set; }
        public string? Carrera { get; set; }
        public string? CreadoPor { get; set; }
    }

    public sealed class EmpleadoDocentePresentacionDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? CodigoEmpleado { get; set; }
        public string? Departamento { get; set; }
        public string? Cargo { get; set; }
        public string? Especialidad { get; set; }
        public string? TipoContrato { get; set; }
    }

    public sealed class CrearEmpleadoDocentePresentacionDto
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? PasswordHash { get; set; }
        public string? CodigoEmpleado { get; set; }
        public string? Departamento { get; set; }
        public string? Cargo { get; set; }
        public string? Especialidad { get; set; }
        public string? TipoContrato { get; set; }
        public string? CreadoPor { get; set; }
    }

    public sealed class EmpleadoAdministrativoPresentacionDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? CodigoEmpleado { get; set; }
        public string? Departamento { get; set; }
        public string? Cargo { get; set; }
        public string? AreaAdministrativa { get; set; }
    }

    public sealed class CrearEmpleadoAdministrativoPresentacionDto
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? PasswordHash { get; set; }
        public string? CodigoEmpleado { get; set; }
        public string? Departamento { get; set; }
        public string? Cargo { get; set; }
        public string? AreaAdministrativa { get; set; }
        public string? CreadoPor { get; set; }
    }
}
