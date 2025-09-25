using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<Marca>> ObtenerPorEstadoAsync (bool estado);
        Task<Marca> ObtenerPorIdAsync ( int id );
        Task<bool> AgregarAsync ( Marca marca );
        Task<bool> ActualizarAsync ( Marca marca );
        Task<bool> CambiarEstadoAsync ( int id, bool estado );
        //Task<Marca?> ObtenerPorIdConProveedoresAsync ( int id );
        Task<Marca?> BuscarPorNombreAsync(string nombre);

    }
}
