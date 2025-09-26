using NSoft.Models.Presupuesto;

namespace NSoft.Repositories.PresupuestoRepository.IRepositories
{
    public interface IManoObraRepository
    {
        Task<ManoDeObra?> ObtenerPorIdConTipoAsync ( int manoObraId );
        Task<ManoDeObra> CrearAsync ( ManoDeObra manoDeObra );
        Task ActualizarAsync ( ManoDeObra manoDeObra );
        Task EliminarAsync ( int manoObraId );
    }
}
