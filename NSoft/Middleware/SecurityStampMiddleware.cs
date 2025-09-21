using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NSoft.Data;

namespace NSoft.Middleware
{
    public class SecurityStampMiddleware
    {
        private readonly RequestDelegate _next;

        // Rutas que NO pasan por el chequeo del sello (swagger, auth, health, preflight CORS)
        private static readonly string[] _excludedStartsWith = { "/swagger" };
        private static readonly string[] _excludedContains = { "/api/auth/login", "/api/auth/register", "/health" };

        public SecurityStampMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower() ?? string.Empty;

            // Excluir rutas utilitarias y preflight
            if (_excludedStartsWith.Any(path.StartsWith) ||
                _excludedContains.Any(path.Contains) ||
                context.Request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            // Si no está autenticado, dejamos que [Authorize] decida (o el endpoint es público)
            if (context.User?.Identity?.IsAuthenticated != true)
            {
                await _next(context);
                return;
            }

            // Claims ya validadas por JwtBearer
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var tokenSecurityStamp = context.User.Claims.FirstOrDefault(c => c.Type == "securityStamp")?.Value;

            if (!int.TryParse(userIdClaim, out var userId) || string.IsNullOrWhiteSpace(tokenSecurityStamp))
            {
                await WriteUnauthorized(context, "Token inválido o faltan claims requeridos.");
                return;
            }

            // Cargar solo lo necesario y NO trackear (perf)
            var db = context.RequestServices.GetRequiredService<ApplicationDbContext>();
            var user = await db.Usuarios
                .AsNoTracking()
                .Select(u => new { u.UsuarioId, u.SecurityStamp, u.Estado })
                .FirstOrDefaultAsync(u => u.UsuarioId == userId);

            if (user is null || user.SecurityStamp != tokenSecurityStamp || !user.Estado)
            {
                await WriteUnauthorized(context, "Token inválido o usuario no autorizado.");
                return;
            }

            await _next(context);
        }

        private static async Task WriteUnauthorized(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = message, timestamp = DateTime.UtcNow });
        }
    }
}
