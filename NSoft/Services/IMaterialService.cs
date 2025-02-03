using NSoft.Models;

namespace NSoft.Services
{
    public interface IMaterialService
    {
        Task<IEnumerable<Material>> ObtenerTodosAsync();
        Task<Material> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Material material);
        Task ActualizarAsync(Material material);
        Task EliminarAsync(int id);
    }
}
