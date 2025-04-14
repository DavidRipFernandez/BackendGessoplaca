using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface IProveedorMarcaRepository
    {
        Task<bool> AgregarAsync ( ProveedorMarca entidad );
        Task<bool> CambiarEstadoAsync ( string proveedorCifId, int marcaId, bool estado );
        Task<IEnumerable<ProveedorMarca>> ObtenerEliminadosAsync ();
        Task<IEnumerable<ProveedorMarca>> ObtenerMarcasPorProveedorAsync ( string proveedorCifId );
        Task<IEnumerable<ProveedorMarca>> ObtenerProveedoresPorMarcaAsync ( int marcaId );
        Task<ProveedorMarca?> ObtenerPorIdAsync ( string proveedorCifId, int marcaId );
    }
}
