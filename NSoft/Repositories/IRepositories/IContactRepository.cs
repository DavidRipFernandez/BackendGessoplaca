using NSoft.Models;

namespace NSoft.Repositories.IRepositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contacto>> ObtenerTodosAsync();
        Task<Contacto> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Contacto contact);
        Task<bool> ActualizarAsync(Contacto contact);
        Task<bool> EliminarAsync(int id);

    }
}
