using NSoft.DTOs;
using NSoft.DTOs.NSoft.DTOs.Presupuesto;

namespace NSoft.Services.PresupuestoService.IPresupuestoServices
{
    public interface ITipoManoObraService
    {
        Task<ApiResponse<List<TipoManoObraDto>>> ListarActivosAsync ();
        Task<ApiResponse<List<TipoManoObraDto>>> ListarInactivosAsync ();
        Task<ApiResponse<TipoManoObraDetalleDto>> ObtenerPorIdAsync ( int id );
        Task<ApiResponse<TipoManoObraDto>> AgregarAsync ( TipoManoObraCreateDto dto );
        Task<ApiResponse<TipoManoObraDto>> ActualizarAsync ( int id, TipoManoObraUpdateDto dto );
        Task<ApiResponse<bool>> EliminarAsync ( int id );
        Task<ApiResponse<bool>> ReactivarAsync ( int id );
    }
}
