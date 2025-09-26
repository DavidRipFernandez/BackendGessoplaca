using NSoft.Models.Presupuesto;

namespace NSoft.Repositories.PresupuestoRepository.IRepositories
{
    public interface IDetalleDescompuestoRepository
    {
        Task<DetalleDescompuesto> ObtenerPorIdAsync ( int detalleDescompuestoId );
        Task AgregarAsync(DetalleDescompuesto detalle);
        Task ActualizarAsync(DetalleDescompuesto detalle);
        Task EliminarAsync ( int detalleDescompuestoId );
    }
}
