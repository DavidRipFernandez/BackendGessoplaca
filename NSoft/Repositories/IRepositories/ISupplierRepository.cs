using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface ISupplierRepository
    {

        Task<IEnumerable<Proveedor>> ObtenerTodosAsync();
        Task<Proveedor> ObtenerPorIdAsync(string id);
        Task AgregarAsync(Proveedor proveedor);
        Task<bool> ActualizarAsync(Proveedor proveedor);
        Task<bool> EliminarAsync(string id);
        Task<Proveedor> ObtenerProveedorConMarcas (string id);

        Task<bool> AgregarMarcaAlProveedor (string proveedorId, int marcaId);
        Task<bool> DarBajaMarcaAlProveedor ( string proveedorId, int marcaId );

    }
}
