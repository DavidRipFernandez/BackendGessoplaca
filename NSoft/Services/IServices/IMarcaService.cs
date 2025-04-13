using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Services.IServices
{
    public interface IMarcaService
    {
        Task<ApiResponse<List<MarcaDto>>> ObtenerActivosAsync ();
        Task<ApiResponse<List<MarcaDto>>> ObtenerEliminadosAsync ();
        Task<ApiResponse<MarcaDto>> ObtenerPorIdAsync ( int id );
        Task<ApiResponse<bool>> AgregarAsync ( MarcaDto marca );
        Task<ApiResponse<bool>> ActualizarAsync ( MarcaDto marca );
        Task<ApiResponse<bool>> EliminarAsync ( int id );
        Task<ApiResponse<bool>> ReactivarAsync ( int id );
    }
}
