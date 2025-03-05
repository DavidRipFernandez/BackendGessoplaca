using NSoft.DTOs;

namespace NSoft.Repositories.IRepositories
{
    public interface IModulePermissionRepository
    {
        Task<ListModulePermissionResponseDTO> GetModulePermissionByRoleAsync(int rolId);
    }
}
