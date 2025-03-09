using Microsoft.AspNetCore.Identity;
using NSoft.Services.IServices;
using NSoft.Repositories.IRepositories;
using NSoft.DTOs;


namespace NSoft.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<UsuarioDTO>>> GetAllUsuariosAsync()
        {
            try
            {
                var usuarios = await _userRepository.GetAllUsuariosAsync();
                if (usuarios==null || !usuarios.Any())
                {
                    return ApiResponse<IEnumerable<UsuarioDTO>>.SuccessResponse(new List<UsuarioDTO>(), "La lista de Usuarios esta vacía");
                }
                return ApiResponse<IEnumerable<UsuarioDTO>>.SuccessResponse(usuarios, null);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en UsuarioService.GetAllUsuariosAsync: {ex.Message}", ex);
                return ApiResponse<IEnumerable<UsuarioDTO>>.ErrorResponse("Error interno al obtener usuarios.", ex.Message, 500);
            }
        }
    }

}
