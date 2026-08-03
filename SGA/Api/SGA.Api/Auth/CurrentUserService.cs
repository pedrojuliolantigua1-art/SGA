using System.Security.Claims;
using SGA.Application.Interfaces.Services;

namespace SGA.Api.Auth
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public int UsuarioId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var claim = user?.FindFirst(ClaimTypes.NameIdentifier) ?? user?.FindFirst("sub");
                return claim is not null && int.TryParse(claim.Value, out var id) ? id : 0;
            }
        }
    }
}