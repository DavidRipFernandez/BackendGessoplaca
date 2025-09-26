using NSoft.DTOs;
using NSoft.Models;
using BCrypt.Net;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NSoft.Services.IServices;
using NSoft.Repositories.IRepositories;

namespace NSoft.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthServices(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginDto loginDto)
        {
            var usuario = await _authRepository.ObtenerUsuarioPorCorreoAsync(loginDto.Correo);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.ContraseñaHash))
            {
                return new ApiResponse<string>("Error", "Credenciales invalidas",null,401,false);
            }
            var token = await GenerateJwtToken(usuario);
            return new ApiResponse<string>("Autenticación exitosa", "Usuario Autenticado Correctamente", token, 200, true);
        }
        public async Task<ApiResponse<List<PermisoDto>>> ObtenerPermisosAsync (int userId)
        {
            var permisos = await _authRepository.ObtenerPermisosPorUsuarioAsync(userId);
            if (permisos == null)
            {
                return new ApiResponse<List<PermisoDto>>("Error", "Usuario no autorizado o desactivado", null, 401, false);
            }
            return new ApiResponse<List<PermisoDto>>("Ok", "Permisos Obtenidos", permisos, 200, true);
        }
        public async Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var usuario = await _authRepository.ObtenerUsuarioPorIdAsync(dto.UserId);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.OldPasswordHash, usuario.ContraseñaHash))
                return new ApiResponse<bool>("Error", "Contraseña incorrecta", false, 401, false);

            usuario.ContraseñaHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPasswordHash);
            usuario.SecurityStamp = Guid.NewGuid().ToString(); // 🔥 Invalida tokens
            await _authRepository.RegistrarUsuarioAsync(usuario); // 🔥 Guardamos el usuario actualizado

            return new ApiResponse<bool>("Contraseña cambiada correctamente", "", true, 200, true);
        }
        public async Task<ApiResponse<string>> RegisterAsync(RegisterDto registerDto)
        {
            if (await _authRepository.UsuarioExisteAsync(registerDto.Correo))
            {
                return new ApiResponse<string>("El correo ya esta en uso", "", null, 400, false);
            }
            try
            {
                var usuario = new Usuario
                {
                    Nombre = registerDto.Nombre,
                    Correo = registerDto.Correo,
                    ContraseñaHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                    Estado = true,
                    RolId = registerDto.RolId,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                await _authRepository.RegistrarUsuarioAsync(usuario);
                return new ApiResponse<string>("Usuario registrado  correctamente", "", null, 200, true);
            }

            catch(Exception ex)
            {
                Console.WriteLine($"❌ Error en RegisterAsync: {ex.Message}");

                return new ApiResponse<string>(
                    "Error interno",
                    $"Excepción: {ex.Message}", // 🔥 Se pasa el mensaje de la excepción
                    null,
                    500,
                    false
                );
            }
        }

        public async Task<string> GenerateJwtToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Correo),
                new Claim("userId", usuario.UsuarioId.ToString()),
                new Claim("rolId", usuario.RolId.ToString()),
                new Claim("securityStamp", usuario.SecurityStamp)
            };

            // 🔹 Agregar permisos del usuario al token
            var permisos = await _authRepository.ObtenerPermisosPorUsuarioAsync(usuario.UsuarioId);
            foreach (var permiso in permisos)
            {
                claims.Add(new Claim("modulo", $"{permiso.Modulo}:{permiso.Permiso}"));
            }

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["JwtSettings:ExpireDays"])),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task InvalidateUserTokens(int userId)
        {
            var usuario = await _authRepository.ObtenerUsuarioPorIdAsync(userId);
            if (usuario != null)
            {
                usuario.SecurityStamp = Guid.NewGuid().ToString();
                await _authRepository.RegistrarUsuarioAsync(usuario);
            }
        }
    }
}
