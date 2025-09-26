using NSoft.Models.Presupuesto;

namespace NSoft.Repositories.PresupuestoRepository.IRepositories
{
    public interface IPresupuestoRepository
    {
        Task<IEnumerable<Presupuesto>> ListarPorEstadoAsync ( string? estado = null );

        Task<Presupuesto?> ObtenerPorIdConDetallesAsync ( int presupuestoId );

        Task<Presupuesto> CrearAsync ( Presupuesto presupuesto );

        Task ActualizarAsync ( Presupuesto presupuesto );

        Task EliminarAsync ( int presupuestoId );

        Task<Presupuesto> ClonarAsync ( int presupuestoOriginalId, string nuevoNombreEmpresa );
    }
}
