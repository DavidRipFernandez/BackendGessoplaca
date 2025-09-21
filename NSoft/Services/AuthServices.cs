using BCrypt.Net;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSoft.Configurations;
using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace NSoft.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly JwtSettings _jwt;                  

        public AuthServices(IAuthRepository authRepository, IOptions<JwtSettings> jwtOptions)
        {
            _authRepository = authRepository;
            _jwt = jwtOptions.Value;

            //esto es para fines de prueba
            if (string.IsNullOrWhiteSpace(_jwt.Key) || _jwt.Key.Length < 32)
                throw new InvalidOperationException("JwtSettings:Key debe existir y tener al menos 32 caracteres.");
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginDto loginDto)
        {
            var usuario = await _authRepository.ObtenerUsuarioPorCorreoAsync(loginDto.Correo);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.ContraseñaHash))
                return new ApiResponse<string>("Error", "Credenciales invalidas", null, 401, false);

            var token = await GenerateJwtToken(usuario);
            return new ApiResponse<string>("Autenticación exitosa", "Usuario Autenticado Correctamente", token, 200, true);
        }

        public async Task<ApiResponse<List<PermisoDto>>> ObtenerPermisosAsync(int userId)
        {
            var permisos = await _authRepository.ObtenerPermisosPorUsuarioAsync(userId);
            if (permisos == null)
                return new ApiResponse<List<PermisoDto>>("Error", "Usuario no autorizado o desactivado", null, 401, false);

            return new ApiResponse<List<PermisoDto>>("Ok", "Permisos Obtenidos", permisos, 200, true);
        }

        public async Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var usuario = await _authRepository.ObtenerUsuarioPorIdAsync(dto.UserId);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.OldPasswordHash, usuario.ContraseñaHash))
                return new ApiResponse<bool>("Error", "Contraseña incorrecta", false, 401, false);

            usuario.ContraseñaHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPasswordHash);
            usuario.SecurityStamp = Guid.NewGuid().ToString();    // invalida tokens
            await _authRepository.RegistrarUsuarioAsync(usuario);  // update/save

            return new ApiResponse<bool>("Contraseña cambiada correctamente", "", true, 200, true);
        }

        public async Task<ApiResponse<string>> RegisterAsync(RegisterDto registerDto)
        {
            if (await _authRepository.UsuarioExisteAsync(registerDto.Correo))
                return new ApiResponse<string>("El correo ya esta en uso", "", null, 400, false);

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
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en RegisterAsync: {ex.Message}");
                return new ApiResponse<string>("Error interno", $"Excepción: {ex.Message}", null, 500, false);
            }
        }

        // ==== AQUÍ el punto clave: firmar con la MISMA config que valida JwtBearer ====
        public async Task<string> GenerateJwtToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            Console.WriteLine($"[JWT] Signing key len={_jwt.Key?.Length ?? 0}");


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, usuario.Correo),
                new("userId", usuario.UsuarioId.ToString()),
                new("rolId",  usuario.RolId.ToString()),
                new("securityStamp", usuario.SecurityStamp),
                // opcionales recomendados:
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var permisos = await _authRepository.ObtenerPermisosPorUsuarioAsync(usuario.UsuarioId);
            if (permisos != null)
            {
                foreach (var p in permisos)
                    claims.Add(new Claim("modulo", $"{p.Modulo}:{p.Permiso}"));
            }

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.ExpireDays),
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
