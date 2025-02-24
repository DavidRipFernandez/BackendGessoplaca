using NSoft.Models;

namespace NSoft.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Proveedor>> ObtenerTodosAsync ();
        Task<Proveedor> ObtenerPorIdAsync ( string id );
        Task AgregarAsync ( Proveedor contacto );
        Task ActualizarAsync ( Proveedor contacto );
        Task EliminarAsync ( string id );
    }
}
