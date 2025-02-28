using Microsoft.IdentityModel.Tokens;
using NSoft.Data;
using System.IdentityModel.Tokens.Jwt;

namespace NSoft.Middleware
{
    public class SecurityStampMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityStampMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            // Excluir cierto endpoints del middleware
            var path = context.Request.Path.Value.ToLower();
            if (path.Contains("/api/auth/login") || path.Contains("/api/auth/register"))
            {
                await _next(context);
                return;
            }

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring(7);
                var jwtHandler = new JwtSecurityTokenHandler();

                try
                {
                    var jwtToken = jwtHandler.ReadToken(token) as JwtSecurityToken;
                    if (jwtToken != null)
                    {
                        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                        var tokenSecurityStamp = jwtToken.Claims.FirstOrDefault(c => c.Type == "securityStamp")?.Value;

                        if (int.TryParse(userIdClaim, out int userId) && !string.IsNullOrEmpty(tokenSecurityStamp))
                        {
                            var user = await dbContext.Usuarios.FindAsync(userId);
                            if (user == null || user.SecurityStamp != tokenSecurityStamp || !user.Estado)
                            {
                                Console.WriteLine($"Token rechazado para el usuario {userId}. SecurityStamp cambiado o usuario desactivado.");
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsync("Token inválido o usuario no autorizado.");
                                return;
                            }
                        }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    Console.WriteLine($"Token inválido: {ex.Message}");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token no válido.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
