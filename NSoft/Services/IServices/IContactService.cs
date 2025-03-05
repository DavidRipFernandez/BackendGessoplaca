using NSoft.Models;

namespace NSoft.Services.IServices
{
    public interface IContactService
    {

        Task<IEnumerable<Contacto>> ObtenerTodosAsync();
        Task<Contacto> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Contacto contacto);
        Task ActualizarAsync(Contacto contacto);
        Task EliminarAsync(int id);

    }
}
