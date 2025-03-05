using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using System.Data.Common;

namespace NSoft.Repositories
{
    public class ContactRepository : IContactRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ContactRepository> _logger;

        public ContactRepository ( ApplicationDbContext context, ILogger<ContactRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> ActualizarAsync ( Contacto contacto )
        {
            try
            {
                var existe = await _context.Contactos.AnyAsync(contact => contact.ContactoId == contacto.ContactoId);
                if ( !existe )
                    throw new KeyNotFoundException($"No se encontró el contacto con el ID {contacto.ContactoId}");

                _context.Contactos.Update( contacto );
                await _context.SaveChangesAsync();
                return true;
            }
            catch ( DbUpdateException dbEx )
            {
                _logger.LogError(dbEx, "Error al actualizar el contacto con ID {ContactoId}", contacto.ContactoId);
                throw new Exception("Error al actualizar el contacto en la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el contacto con ID {ContactoId}", contacto.ContactoId);
                throw new Exception("Error inesperado al actualizar el contacto.", ex);
            }
        }

        public async Task AgregarAsync ( Contacto contact )
        {
            try
            {
                await _context.Contactos.AddAsync(contact);
                await _context.SaveChangesAsync();
            }
            catch( DbUpdateException dbEx )
            {
                _logger.LogError(dbEx,"Error al agregar el contacto a la base de datos.");
                throw new Exception("Error al agregar el contacto a la base de datos.", dbEx);
            }
            catch( Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar el contacto.");
                throw new Exception("Error inesperado al agregar el contacto", ex);
            }
        }

        public async Task<bool> EliminarAsync ( int id )
        {
            try
            {
                var contacto = await _context.Contactos.FindAsync(id) ?? throw new KeyNotFoundException($"No se encontro el contacto con ID {id}");
                
                _context.Contactos.Remove(contacto);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al eliminar el contacto con ID {ContactoId}", id);
                throw new Exception("Error al eliminar el contacto en la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar el contacto con ID {ContactoId}", id);
                throw new Exception("Error inesperado al eliminar el contacto.", ex);
            }
        }

        public async Task<Contacto> ObtenerPorIdAsync ( int id )
        {
            try
            {
                var contacto = await _context.Contactos.FindAsync(id) ?? throw new KeyNotFoundException($"No se encontró el contacto con Id {id}");

                return contacto;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el contacto con ID {ContactoId}", id);
                throw new Exception("Error inesperado al obtener el contacto.", ex);
            }
        }

        public async Task<IEnumerable<Contacto>> ObtenerTodosAsync ()
        {
            try
            {
                return await _context.Contactos.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los contactos.");
                throw new Exception("Error inesperado al obtener los contactos.", ex);
            }
        }

    }
}
