using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Services.IServices
{
    public interface IMaterialService
    {
        Task<ApiResponse<List<MaterialDto>>> ObtenerActivosAsync ();
        Task<ApiResponse<List<MaterialDto>>> ObtenerEliminadosAsync ();
        Task<ApiResponse<MaterialDto>> ObtenerPorIdConCategoriaAsync ( int id );
        Task<ApiResponse<bool>> AgregarAsync ( MaterialDto dto );
        Task<ApiResponse<bool>> ActualizarAsync ( MaterialDto dto );
        Task<ApiResponse<bool>> EliminarAsync ( int id );
        Task<ApiResponse<bool>> ReactivarAsync ( int id );
    }
}
