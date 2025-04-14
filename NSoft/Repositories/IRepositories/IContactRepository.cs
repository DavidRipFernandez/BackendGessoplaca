using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contacto>> ObtenerPorEstadoConProveedorAsync ( bool estado );
        Task<Contacto> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Contacto>> ObtenerPorNombreAsync ( string nombre );
        Task<bool> AgregarAsync(Contacto contact);
        Task<bool> ActualizarAsync(Contacto contact);
        Task<bool> CambiarEstadoAsync ( int id, bool nuevoEstado );

    }
}
