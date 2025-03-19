using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Services.IServices
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaDto>> ObtenerActivosAsync ();
        Task<IEnumerable<MarcaDto>> ObtenerEliminadosAsync ();
        Task<MarcaDto> ObtenerPorIdAsync ( int id );
        Task<bool> AgregarAsync ( MarcaDto marca );
        Task<bool> ActualizarAsync ( MarcaDto marca );
        Task<bool> EliminarAsync ( int id );
        Task<MarcaDto?> ObtenerPorIdConProveedoresAsync ( int id );
    }
}
