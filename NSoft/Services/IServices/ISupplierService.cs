using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Services.IServices
{
    public interface ISupplierService
    {
        Task<IEnumerable<ProveedorDto>> ObtenerTodosAsync();
        Task<ProveedorDto> ObtenerPorIdAsync(string id);
        Task AgregarAsync(ProveedorDto contacto);
        Task ActualizarAsync(ProveedorDto contacto);
        Task EliminarAsync(string id);
        Task<bool> AgregarMarcaAlProveedor ( string proveedorId, int marcaId );
        Task<bool> DarBajaMarcaAlProveedor ( string proveedorId, int marcaId );
    }
}
