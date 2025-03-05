using NSoft.DTOs;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class ModulePermissionService : IModulePermissionService
    {
        private readonly IModulePermissionRepository _modulePermissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<ModulePermissionService> _logger;

        public ModulePermissionService(IModulePermissionRepository modulePermissionRepository, ILogger<ModulePermissionService> logger, IRoleRepository roleRepository)
        {
            _modulePermissionRepository = modulePermissionRepository;
            _logger = logger;
            _roleRepository = roleRepository;
        }

        public async Task<ApiResponse<ListModulePermissionResponseDTO>> GetModulePermissionsByRoleAsync(int rolId)
        {
            //validar que el rol exista 
            try
            {
                var existeRol = await _roleRepository.RoleExistsAsync(rolId);
                if (!existeRol)
                {
                    return ApiResponse<ListModulePermissionResponseDTO>.ErrorResponse("El rol id no existe", "No se encontró el rol en la base de datos.", 404);
                }
                var result = await _modulePermissionRepository.GetModulePermissionByRoleAsync(rolId);
                return ApiResponse<ListModulePermissionResponseDTO>.SuccessResponse(result, "");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en ModuleService.GetModulePermissionsByRoleAsync: {ex.Message}", ex);
                return ApiResponse<ListModulePermissionResponseDTO>.ErrorResponse("Error interno al obtener módulos.", ex.Message, 500);
            }
        }
    }
}
