namespace SGA.Domain.Entities.Usuarios
{
    /// <summary>
    /// Usuario con rol de Administrador de Transporte (RolSistema = 1).
    /// Gestiona el catálogo, planifica viajes y accede a los reportes operativos.
    /// </summary>
    public class AdministradorTransporte : UsuarioTransporte
    {
        public string? Departamento { get; set; }
        public string? Cargo { get; set; }
    }
}
