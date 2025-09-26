using NSoft.DTOs;
using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface IPrecioTarifaRepository
    {
        Task<bool> GuardarPrecioAsync ( PrecioTarifa precio ); // upsert

        Task<IEnumerable<PrecioTarifaResumenDto>> ObtenerPreciosPorProveedorAsync ( string proveedorCifId );
        Task<IEnumerable<PrecioTarifaResumenDto>> ObtenerPreciosPorMarcaAsync ( int marcaId ); 
        Task<PrecioTarifa?> ObtenerPrecioMasBajoAsync ( int materialId );
        Task<PrecioTarifa?> ObtenerPrecioAsync ( int materialId, int marcaId, string proveedorCif );

        Task<bool> ExisteAsync ( int materialId, string proveedorCifId, int marcaId );

        Task<bool> GuardarMasivoAsync ( List<PrecioTarifa> precios );
    }
}
