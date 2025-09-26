using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface ICategoriaMaterialRepository
    {
        Task<IEnumerable<CategoriaMaterial>> ObtenerPorEstadoAsync ( bool estado );
        Task<CategoriaMaterial?> ObtenerPorIdAsync ( int id );
        Task<CategoriaMaterial?> ObtenerPorIdConMaterialesAsync ( int id );
        Task AgregarAsync ( CategoriaMaterial categoria );
        Task ActualizarAsync ( CategoriaMaterial categoria );
        Task CambiarEstadoAsync ( int id, bool estado );
    }
}
