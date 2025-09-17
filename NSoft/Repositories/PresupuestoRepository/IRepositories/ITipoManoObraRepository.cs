using NSoft.Models;
using NSoft.Models.Presupuesto;

namespace NSoft.Repositories.PresupuestoRepository.IRepositories
{
    public interface ITipoManoObraRepository
    {
        Task<List<TipoManoObra>> ListarPorEstadoAsync ( bool estado );
        Task<TipoManoObra?> ObtenerPorIdAsync ( int id);
        Task AgregarAsync ( TipoManoObra entity );
        Task ActualizarAsync ( TipoManoObra entity );
        Task CambiarEstadoAsync ( int id, bool estado );
    }
}
