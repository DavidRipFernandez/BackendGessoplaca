using NSoft.DTOs;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<RoleDTO>>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllRolesAsync();
                if (roles == null || !roles.Any())
                {
                    return ApiResponse<IEnumerable<RoleDTO>>.SuccessResponse(new List<RoleDTO>(), "No se encontraron roles.");
                }
                return ApiResponse<IEnumerable<RoleDTO>>.SuccessResponse(roles, "Roles obtenidos con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en RoleService.GetAllRolesAsync: {ex.Message}", ex);
                return ApiResponse<IEnumerable<RoleDTO>>.ErrorResponse("Error interno al obtener roles.", ex.Message, 500);
            }
        }
    }
}
