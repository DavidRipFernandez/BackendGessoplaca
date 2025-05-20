using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Services.IServices
{
    public interface ISupplierService
    {

        Task<ApiResponse<List<ProveedorDto>>> ObtenerTodosAsync ();
        Task<ApiResponse<List<ProveedorDto>>> ObtenerActivosAsync ();
        Task<ApiResponse<List<ProveedorDto>>> ObtenerEliminadosAsync ();
        Task<ApiResponse<ProveedorDto>> ObtenerProveedorConRelacionesAsync ( string id);
        Task<ApiResponse<bool>> AgregarAsync (ProveedorDto proveedor);
        Task<ApiResponse<bool>> ActualizarAsync (ProveedorDto proveedor);
        Task<ApiResponse<bool>> EliminarAsync (string id);
        Task<ApiResponse<bool>> ReactivarAsync ( string id );
    }
}