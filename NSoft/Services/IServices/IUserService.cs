using NSoft.DTOs;

namespace NSoft.Services.IServices
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<UsuarioDTO>>> GetAllUsuariosAsync();
    }
}
