using NSoft.DTOs;

namespace NSoft.Services.IServices
{
    public interface IRoleService
    {
        Task<ApiResponse<IEnumerable<RoleDTO>>> GetAllRolesAsync ();
        Task<ApiResponse<RoleDTO>> CreateRoleAsync(RoleCreatedDTO dto);
    }
}
