using NSoft.Models;

namespace NSoft.Repositories
{
    public interface ISupplierRepository
    {

        Task<IEnumerable<Proveedor>> ObtenerTodosAsync ();
        Task<Proveedor> ObtenerPorIdAsync ( string id );
        Task AgregarAsync ( Proveedor proveedor );
        Task<bool> ActualizarAsync ( Proveedor proveedor );
        Task<bool> EliminarAsync ( string id );

    }
}
