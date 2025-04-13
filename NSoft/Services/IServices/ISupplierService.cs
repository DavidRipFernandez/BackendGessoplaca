using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Services.IServices
{
    public interface ISupplierService
    {
        Task<ApiResponse<List<ProveedorDto>>> ObtenerActivosAsync ();
        Task<ApiResponse<List<ProveedorDto>>> ObtenerEliminadosAsync ();
        Task<ApiResponse<ProveedorDto>> ObtenerPorIdAsync(string id);
        Task<ApiResponse<ProveedorDto>> ObtenerProveedorConMarcas ( string id );
        Task<ApiResponse<bool>> AgregarAsync (ProveedorDto proveedor);
        Task<ApiResponse<bool>> ActualizarAsync (ProveedorDto proveedor);
        Task<ApiResponse<bool>> EliminarAsync (string id);
        Task<ApiResponse<bool>> ReactivarAsync ( string id );
        Task<ApiResponse<bool>> AgregarMarcaAlProveedor ( string proveedorId, int marcaId );
        Task<ApiResponse<bool>> DarBajaMarcaAlProveedor ( string proveedorId, int marcaId );
    }
}