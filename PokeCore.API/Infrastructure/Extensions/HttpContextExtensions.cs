using System.Security.Claims;

namespace PokeCore.API.Infrastructure.Extensions;

public static class HttpContextExtensions
{
    public static Guid? ObtenerUsuarioId(this ClaimsPrincipal user)
    {
        var sub = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? user.FindFirst("sub")?.Value;

        return Guid.TryParse(sub, out var guid) ? guid : null;
    }
}