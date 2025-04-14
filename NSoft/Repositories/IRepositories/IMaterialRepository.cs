using NSoft.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSoft.Repositories.IRepositories
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> ObtenerPorEstadoAsync ( bool estado );
        Task<Material?> ObtenerPorIdConCategoriaAsync ( int id );
        Task<Material?> BuscarPorNombreAsync ( string nombre );
        Task<bool> AgregarAsync ( Material material );
        Task<bool> ActualizarAsync ( Material material );
        Task<bool> CambiarEstadoAsync ( int id, bool estado );
    }
}
