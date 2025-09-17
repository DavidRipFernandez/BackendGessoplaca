using NSoft.DTOs;

namespace NSoft.Services.PresupuestoService.IPresupuestoServices
{
    public interface IDescompuestoService
    {
        Task<ApiResponse<List<DescompuestoDto>>> ObtenerPlantillasAsync ();
        Task<ApiResponse<DescompuestoDto>> ObtenerConDetallesAsync ( int descompuestoId );
        Task<ApiResponse<List<DescompuestoDto>>> BuscarPorNombreAsync ( string nombre );
        Task<ApiResponse<DescompuestoDto>> CrearAsync ( DescompuestoCreacionDto descompuestoDto );
        Task<ApiResponse<bool>> ActualizarParcialAsync ( int descompuestoId, DescompuestoEdicionParcialDto descompuestoDto );
        Task<ApiResponse<bool>> ActualizarCompletoAsync ( int descompuestoId, DescompuestoDto descompuestoDto );
        Task<ApiResponse<bool>> EliminarAsync ( int descompuestoId );
    }
}
