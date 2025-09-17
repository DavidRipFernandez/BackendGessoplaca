using NSoft.Models.Presupuesto;

namespace NSoft.Repositories.PresupuestoRepository.IRepositories
{
    public interface IDescompuestoRepository
    {
        Task<IEnumerable<Descompuesto>> ObtenerPlantillas();
        Task<Descompuesto> AgregarAsync (Descompuesto descompuesto);
        Task ActualizarParcialAsync ( int descompuestoId, string titulo, string descripcion, string unidadMedida, decimal beneficio, decimal manoDeObra, decimal gastoAdministrativo );
        Task ActualizarCompletoAsync ( Descompuesto descompuesto );
        Task EliminarAsync ( int idDescompuesto );
        Task<Descompuesto?> ObtenerConDetallesAsync ( int idDescompuesto );
        Task<IEnumerable<Descompuesto>> BuscarPorNombreAsync ( string nombre );

    }
}
