using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Proveedor>> ObtenerTodosAsync ();
        Task<SupplierDTO> ObtenerPorIdAsync ( string id );
        Task AgregarAsync ( Proveedor contacto );
        Task ActualizarAsync ( Proveedor contacto );
        Task EliminarAsync ( string id );
    }
}
