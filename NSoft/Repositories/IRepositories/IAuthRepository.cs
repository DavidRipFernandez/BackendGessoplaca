using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface IAuthRepository
    {
        Task<Usuario> ObtenerUsuarioPorCorreoAsync(string correo);
        Task<Usuario> ObtenerUsuarioPorIdAsync(int userId);
        Task<List<PermisoDto>> ObtenerPermisosPorUsuarioAsync(int userId);
        Task<bool> UsuarioExisteAsync(string correo);
        Task RegistrarUsuarioAsync(Usuario usuario);
    }
}
