using NSoft.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSoft.Repositories
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> ObtenerTodosAsync();
        Task<Material> ObtenerPorIdAsync(int id);
        Task AgregarAsync (Material material);
        Task ActualizarAsync (Material material);
        Task EliminarAsync (int id);
    }
}
