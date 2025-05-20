using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Proveedor>> ObtenerProveedoresAsync ( bool? estado );
        Task<Proveedor?> ObtenerProveedorConRelacionesAsync ( string id);
        Task<Proveedor?> ObtenerPorIdAsync (string id);
        Task<bool> AgregarAsync(Proveedor proveedor);
        Task<bool> ActualizarAsync(Proveedor proveedor);
        Task<bool> CambiarEstadoAsync ( string id, bool estado );
    }
}
