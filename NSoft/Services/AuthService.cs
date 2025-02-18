using Microsoft.IdentityModel.Tokens;
using NSoft.Data;
using NSoft.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NSoft.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerateJwtToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Obtener los módulos y permisos del usuario desde la base de datos

            var rolesModulos = _context.RolesModulos
                .Where(rm => rm.RolId == usuario.RolId)
                .Select(rm => new { rm.Modulo.ModuloCodigo, rm.TipoPermiso.NombrePermiso })
                .ToList();

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Correo),
            new Claim("userId", usuario.UsuarioId.ToString()),
            new Claim("rolId", usuario.RolId.ToString()),
            new Claim("securityStamp", usuario.SecurityStamp)
        };
            //Agregar módulos y permisos como claims en el JWT 

            foreach (var permiso in rolesModulos)
            {
                claims.Add(new Claim("modulo",$"{permiso.ModuloCodigo}:{permiso.NombrePermiso}"));
            }

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["JwtSettings:ExpireDays"])),
                signingCredentials : creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void InvalidateUserTokens(int userId)
        {
            var usuario = _context.Usuarios.Find(userId);
            if (usuario != null)
            {
                usuario.SecurityStamp = Guid.NewGuid().ToString();
                _context.SaveChanges();
            }
        }

    }
}
