using PokeCore.API.Auth.Interfaces;

namespace PokeCore.API.Api.Middlewares;

public class SupabaseTokenMiddleware
{
    private readonly RequestDelegate _next;

    public SupabaseTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAuthService authService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

        if (!string.IsNullOrWhiteSpace(token))
        {
            var isValid = await authService.ValidateTokenAsync(token);
            if (!isValid)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token inválido o expirado en Supabase.");
                return;
            }
        }

        await _next(context);
    }
}