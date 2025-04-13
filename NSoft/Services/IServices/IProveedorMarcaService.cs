using NSoft.DTOs;

namespace NSoft.Services.IServices
{
    public interface IProveedorMarcaService
    {
        Task<ApiResponse<bool>> AgregarAsync ( ProveedorMarcaDto relacion );
        Task<ApiResponse<bool>> EliminarAsync ( string proveedorCifId, int marcaId );
        Task<ApiResponse<bool>> ReactivarAsync ( string proveedorCifId, int marcaId );
        Task<ApiResponse<ProveedorConMarcasDto>> ObtenerMarcasPorProveedorAsync ( string proveedorCifId );
        Task<ApiResponse<MarcaConProveedoresDto>> ObtenerProveedoresPorMarcaAsync ( int marcaId );
        Task<ApiResponse<List<ProveedorMarcaDto>>> ObtenerEliminadosAsync ();
    }
}
