using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using NSoft.Data;
using NSoft.Services;
using NSoft.DTOs;
using NSoft.Models;
using System.Text;

namespace NSoft.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authService;

        public AuthController(ApplicationDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            /*
            var usuario = _context.Usuarios.SingleOrDefault(u => u.Correo == loginDto.Correo);
            if (usuario == null || usuario.ContraseñaHash != loginDto.PasswordHash)
                return Unauthorized("Credenciales inválidas");

            var token = _authService.GenerateJwtToken(usuario);
            return Ok(new { token });
            */
            var usuario = _context.Usuarios.SingleOrDefault(u => u.Correo == loginDto.Correo);

            // Validar si el usuario existe
            if (usuario == null)
                return Unauthorized("Credenciales inválidas");

            // Comparar la contraseña ingresada con el hash almacenado
            if (!BCrypt.Net.BCrypt.Verify(loginDto.PasswordHash, usuario.ContraseñaHash))
                return Unauthorized("Credenciales inválidas");

            // Generar el token JWT
            var token = _authService.GenerateJwtToken(usuario);
            return Ok(new { token });
        }
        [Authorize]
        [HttpGet("mis-permisos")]
        public IActionResult ObtenerPermisos()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userIdClaim == null)
                return Unauthorized("Usuario no autenticado");

            int userId = int.Parse(userIdClaim);
            var usuario = _context.Usuarios.Find(userId);
            if (usuario == null || !usuario.Estado)
                return Unauthorized("Usuario no autorizado o desactivado");

            var permisos = _context.RolesModulos
                .Where(rm => rm.RolId == usuario.RolId)
                .Select(rm => new
                {
                    Modulo = rm.Modulo.ModuloCodigo,
                    Permiso = rm.TipoPermiso.NombrePermiso
                })
                .ToList();

            return Ok(permisos);
        }
        [Authorize]
        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var usuario = _context.Usuarios.Find(dto.UserId);
            if (usuario == null || usuario.ContraseñaHash != dto.OldPasswordHash)
                return Unauthorized("Contraseña incorrecta");

            usuario.ContraseñaHash = dto.NewPasswordHash;
            _authService.InvalidateUserTokens(usuario.UsuarioId); // 🔥 Forzar logout
            return Ok("Contraseña cambiada, sesión cerrada en todos los dispositivos");
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            // 📌 Verificar si el correo ya está registrado
            if (_context.Usuarios.Any(u => u.Correo == registerDto.Correo))
                return BadRequest("El correo ya está en uso");

            // 📌 Crear el hash de la contraseña
            //var passwordHash = HashPassword(registerDto.Password);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            // 📌 Crear el usuario
            var usuario = new Usuario
            {
                Nombre = registerDto.Nombre,
                Correo = registerDto.Correo,
                ContraseñaHash = passwordHash,
                Estado = true,
                RolId = registerDto.RolId,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return Ok("Usuario registrado correctamente");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

    }
}
