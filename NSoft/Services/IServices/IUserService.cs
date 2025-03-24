using NSoft.DTOs;

namespace NSoft.Services.IServices
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<UsuarioDTO>>> GetAllUsuariosAsync();
        Task<ApiResponse<UsuarioDTO>> UpdateUsuarioAsync(UserUpdateDTO dto);
        Task<ApiResponse<string>> DeleteUserAsync(int userId);
    }
}
