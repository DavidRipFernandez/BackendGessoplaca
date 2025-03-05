using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync();
    }
}
