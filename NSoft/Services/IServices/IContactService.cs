using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Services.IServices
{
    public interface IContactService
    {

        Task<ApiResponse<List<ContactoDto>>> ObtenerActivosPorProveedorAsync ();
        Task<ApiResponse<List<ContactoDto>>> ObtenerEliminadosPorProveedorAsync ();
        Task<ApiResponse<ContactoDto>> ObtenerPorIdAsync(int id);
        Task<ApiResponse<List<ContactoDto>>> ObtenerPorNombreAsync ( string nombre );
        Task<ApiResponse<bool>> AgregarAsync ( ContactoDto contacto );
        Task<ApiResponse<bool>> ActualizarAsync (ContactoDto contacto);
        Task<ApiResponse<bool>> EliminarAsync (int id);
        Task<ApiResponse<bool>> ReactivarAsync (int id);

    }
}
