using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Autentica al usuario y genera un token JWT si las credenciales son correctas.
        /// </summary>
        Task<ApiResponse<string>> LoginAsync(LoginDto loginDto);

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        Task<ApiResponse<string>> RegisterAsync(RegisterDto registerDto);

        /// <summary>
        /// Cambia la contraseña de un usuario autenticado.
        /// </summary>
        Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordDto dto);

        /// <summary>
        /// Obtiene los permisos del usuario autenticado.
        /// </summary>
        Task<ApiResponse<List<PermisoDto>>> ObtenerPermisosAsync(int userId);

        /// <summary>
        /// Genera un token JWT para un usuario autenticado.
        /// </summary>
        Task <string> GenerateJwtToken(Usuario usuario);

        /// <summary>
        /// Invalida los tokens del usuario al cambiar su `SecurityStamp`.
        /// </summary>
        Task InvalidateUserTokens(int userId);

    }
}
