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
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsync("Token inválido o usuario no autorizado.");
                                return;
                            }
                        }
                    }
                }
                catch (SecurityTokenException)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token no válido.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
