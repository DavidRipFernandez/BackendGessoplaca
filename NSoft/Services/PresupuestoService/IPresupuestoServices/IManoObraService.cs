using NSoft.DTOs;

namespace NSoft.Services.PresupuestoService.IPresupuestoServices
{
    public interface IManoObraService
    {

        Task<ApiResponse<ManoDeObraDto>> ObtenerPorIdAsync ( int manoObraId );
        Task<ApiResponse<ManoDeObraDto>> CrearAsync ( ManoDeObraCreacionDto dto );
        Task<ApiResponse<bool>> ActualizarAsync ( int manoObraId, ManoDeObraEdicionDto dto );
        Task<ApiResponse<bool>> EliminarAsync ( int manoObraId );

    }
}
