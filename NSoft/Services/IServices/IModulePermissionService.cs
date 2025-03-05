using NSoft.DTOs;

namespace NSoft.Services.IServices
{
    public interface IModulePermissionService
    {
        Task<ApiResponse<ListModulePermissionResponseDTO>> GetModulePermissionsByRoleAsync(int rolId);
    }
}
