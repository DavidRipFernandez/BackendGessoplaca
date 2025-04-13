using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface ISupplierRepository
    {

        Task<IEnumerable<Proveedor>> ObtenerPorEstadoAsync ( bool estado );
        Task<Proveedor> ObtenerProveedorConContactosAsync ( string id);
        Task<bool> AgregarAsync(Proveedor proveedor);
        Task<bool> ActualizarAsync(Proveedor proveedor);
        Task<bool> CambiarEstadoAsync ( string id, bool estado )
        Task<Proveedor> ObtenerProveedorConMarcas (string id);
    }
}
