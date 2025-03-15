using Microsoft.AspNetCore.Identity;
using NSoft.Services.IServices;
using NSoft.Repositories.IRepositories;
using NSoft.DTOs;
using NSoft.Repositories;


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

        public async Task<ApiResponse<UsuarioDTO>> UpdateUsuarioAsync(UserUpdateDTO updateDtoUser)
        {
            if (updateDtoUser == null)
            {
                return ApiResponse<UsuarioDTO>.ErrorResponse(
                    "El objeto enviado es nulo.",
                    "Se requiere información para actualizar un usuario.",
                    400);
            }
            if (updateDtoUser.UsuarioId <= 0)
            {
                return ApiResponse<UsuarioDTO>.ErrorResponse(
                    "El UsuarioId es inválido.",
                    "El UsuarioId debe ser mayor a 0.",
                    400);
            }
            if (string.IsNullOrWhiteSpace(updateDtoUser.Nombre) || string.IsNullOrWhiteSpace(updateDtoUser.Correo) || string.IsNullOrWhiteSpace(updateDtoUser.Contraseña))
            {
                return ApiResponse<UsuarioDTO>.ErrorResponse(
                    "Datos inválidos.",
                    "El nombre, el correo y la contraseña son obligatorios.",
                400);
            }
           
            try
            {
                
                var updatedUser = await _userRepository.UpdateUsuarioAsync(updateDtoUser);
                return ApiResponse<UsuarioDTO>.SuccessResponse(updatedUser, "Usuario actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en UserService.UpdateUsuarioAsync: {ex.Message}", ex);
                return ApiResponse<UsuarioDTO>.ErrorResponse("Error interno al actualizar el usuario.", ex.Message, 500);
            }
        }
    }

}
