using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface ICategoriaMaterialRepository
    {
        Task<IEnumerable<CategoriaMaterial>> ObtenerPorEstadoAsync ( bool estado );
        Task<CategoriaMaterial?> ObtenerPorIdAsync ( int id );
        Task<CategoriaMaterial?> ObtenerPorIdConMaterialesAsync ( int id );
        Task<bool> AgregarAsync ( CategoriaMaterial categoria );
        Task<bool> ActualizarAsync ( CategoriaMaterial categoria );
        Task<bool> CambiarEstadoAsync ( int id, bool estado );
    }
}
