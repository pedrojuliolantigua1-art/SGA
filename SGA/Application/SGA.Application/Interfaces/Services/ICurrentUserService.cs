namespace SGA.Application.Interfaces.Services
{
    /// Expone el ID del usuario autenticado actual extraído del JWT de la petición,
    /// para que cualquier servicio pueda registrar auditoría con el actor correcto sin que cada
    /// método necesite recibirlo como parámetro
    public interface ICurrentUserService
    {
        int UsuarioId { get; }
    }
}