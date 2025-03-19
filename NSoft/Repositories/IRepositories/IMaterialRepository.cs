using NSoft.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSoft.Repositories.IRepositories
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> ObtenerTodosAsync();
        Task<Material> ObtenerPorIdAsync(int id);
        Task<bool> AgregarAsync(Material material);
        Task<bool> ActualizarAsync(Material material);
        Task<bool> EliminarAsync(int id);
    }
}
