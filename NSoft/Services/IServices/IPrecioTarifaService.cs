using NSoft.DTOs;

namespace NSoft.Services.IServices
{
    public interface IPrecioTarifaService
    {
        Task<ApiResponse<bool>> GuardarPrecioAsync ( PrecioTarifaDto dto );
        Task<ApiResponse<PreciosProveedorDto>> ObtenerPreciosPorProveedorAsync ( string proveedorCifId );
        Task<ApiResponse<MarcaConPreciosDto>> ObtenerPrecioPorMarcaAsync ( int marcaId );
        Task<ApiResponse<PrecioTarifaDto>> ObtenerPrecioMasBajoDeMaterialAsync ( int materialId );
        Task<ApiResponse<bool>> GuardarMasivoAsync ( List<PrecioTarifaDto> precios );
        Task<ApiResponse<object>> CargarPreciosPorNombresAsync(
         string proveedorCifId,
         string? empresa,
         List<CargaPrecioItemRequestDto> filas);


    }
}
