using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<Marca>> ObtenerActivosAsync ();
        Task<IEnumerable<Marca>> ObtenerEliminadosAsync ();
        Task<Marca> ObtenerPorIdAsync ( int id );
        Task<bool> AgregarAsync ( Marca marca );
        Task<bool> ActualizarAsync ( Marca marca );
        Task<bool> EliminarAsync ( int id );
        Task<Marca?> ObtenerPorIdConProveedoresAsync ( int id );

    }
}
