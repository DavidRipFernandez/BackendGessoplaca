using NSoft.DTOs;

namespace NSoft.Repositories.IRepositories
{
    public interface IRoleRepository
    {
        Task<bool> RoleExistsAsync(int rolId);
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO> CreateRoleAsync(RoleCreatedDTO dto);
    }
}
