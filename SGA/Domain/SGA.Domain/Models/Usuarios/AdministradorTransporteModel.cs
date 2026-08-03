namespace SGA.Domain.Models.Usuarios
{
    /// <summary>
    /// Proyección de solo lectura de un AdministradorTransporte.
    /// Returned by the repository; never written back to the database.
    /// </summary>
    public class AdministradorTransporteModel : UsuarioModel
    {
        public string? Departamento { get; set; }
        public string? Cargo { get; set; }
    }
}
