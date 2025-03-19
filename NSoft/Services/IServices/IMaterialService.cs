using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Services.IServices
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialDto>> ObtenerTodosAsync();
        Task<MaterialDto> ObtenerPorIdAsync(int id);
        Task<bool> AgregarAsync( MaterialDto material );
        Task<bool> ActualizarAsync ( MaterialDto material );
        Task<bool> EliminarAsync (int id);
    }
}
