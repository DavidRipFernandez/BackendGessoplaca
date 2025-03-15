using NSoft.DTOs;
using NSoft.Models;
using System.Collections;

namespace NSoft.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync();
        Task<UsuarioDTO> UpdateUsuarioAsync(UserUpdateDTO dto);
        Task<bool> UserExistAsync(int userId);
    }
}
