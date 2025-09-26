using NSoft.DTOs;

namespace NSoft.Services.PresupuestoService.IPresupuestoServices
{
    public interface IDetalleDescompuestoService
    {
        Task<ApiResponse<DetalleDescompuestoDto>> CrearAsync ( DetalleDescompuestoCreacionDto dto );         // ADDED
        Task<ApiResponse<bool>> ActualizarAsync ( int detalleDescompuestoId, DetalleDescompuestoEdicionDto dto ); // ADDED
        Task<ApiResponse<bool>> EliminarAsync ( int detalleDescompuestoId );
    }
}
