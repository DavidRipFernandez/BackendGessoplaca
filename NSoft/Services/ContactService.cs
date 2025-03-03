using NSoft.Models;
using NSoft.Repositories;

namespace NSoft.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService( IContactRepository contactRepository )
        {
            _contactRepository = contactRepository;
        }

        public async Task ActualizarAsync ( Contacto contacto )
        {
            ArgumentNullException.ThrowIfNull(contacto);

            try
            {
                var actualizado = await _contactRepository.ActualizarAsync( contacto );
                if (!actualizado)
                    throw new KeyNotFoundException($"No se encontró el contacto con ID {contacto.ContactoId} para actualizar");

            }
            catch ( Exception ex )
            {
                throw new Exception("Error al actualizar el contacto.", ex);
            }
        }

        public async Task AgregarAsync ( Contacto contacto )
        {
            ArgumentNullException.ThrowIfNull (contacto);

            try
            {
                await _contactRepository.AgregarAsync(contacto);
            }
            catch( Exception ex )
            {
                throw new Exception("Error al agregar el contacto.", ex);
            }

        }

        public async Task EliminarAsync ( int id )
        {
            try
            {
                var eliminado = await _contactRepository.EliminarAsync(id);
                if (!eliminado)
                    throw new KeyNotFoundException($"CONTROLLER| No se encontró el contacto con ID {id} para eliminar.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el contacto.", ex);
            }
        }

        public async Task<Contacto> ObtenerPorIdAsync ( int id )
        {
            try
            {
                return await _contactRepository.ObtenerPorIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el contacto.", ex);
            }
        }

        public async Task<IEnumerable<Contacto>> ObtenerTodosAsync ()
        {
            try
            {
                return await _contactRepository.ObtenerTodosAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de contactos.", ex);
            }
        }
    }
}
