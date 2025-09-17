using NSoft.DTOs;

namespace NSoft.Services.PresupuestoService.IPresupuestoService
{
    public interface IPresupuestoService
    {
        // MÉTODOS DE LISTADO ESPECÍFICOS
        Task<ApiResponse<List<PresupuestoDto>>> ListarPendientesAsync ();
        Task<ApiResponse<List<PresupuestoDto>>> ListarEnCursoAsync ();
        Task<ApiResponse<List<PresupuestoDto>>> ListarRechazadosAsync ();

        Task<ApiResponse<PresupuestoDto>> ObtenerPorIdAsync ( int presupuestoId );
        Task<ApiResponse<PresupuestoDto>> CrearAsync ( PresupuestoDto presupuestoDto );
        Task<ApiResponse<bool>> ActualizarAsync ( int presupuestoId, PresupuestoDto presupuestoDto );
        Task<ApiResponse<bool>> EliminarAsync ( int presupuestoId );
        Task<ApiResponse<PresupuestoDto>> ClonarAsync ( int presupuestoOriginalId, string nombre );
    }
}
