using NSoft.DTOs;

namespace NSoft.Services.IServices
{
    public interface ICategoriaMaterialService
    {
        Task<ApiResponse<List<CategoriaMaterialDto>>> ObtenerActivosAsync ();
        Task<ApiResponse<List<CategoriaMaterialDto>>> ObtenerEliminadosAsync ();
        Task<ApiResponse<CategoriaMaterialDto>> ObtenerPorIdAsync ( int id );
        Task<ApiResponse<CategoriaMaterialDto>> ObtenerPorIdConMaterialesAsync ( int id );
        Task<ApiResponse<bool>> AgregarAsync ( CategoriaMaterialDto categoriaDto );
        Task<ApiResponse<bool>> ActualizarAsync ( CategoriaMaterialDto categoriaDto );
        Task<ApiResponse<bool>> EliminarAsync ( int id );
        Task<ApiResponse<bool>> ReactivarAsync ( int id );
    }
}
