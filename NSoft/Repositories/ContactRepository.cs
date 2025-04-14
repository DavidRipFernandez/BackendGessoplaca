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

        public async Task<IEnumerable<Contacto>> ObtenerPorEstadoConProveedorAsync ( bool estado )
        {
            try
            {
                return await _context.Contactos
                    .AsNoTracking()
                    .Include(c => c.Proveedor)
                    .Where(c => c.Estado == estado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                var estadoTexto = estado ? "activos" : "eliminados";
                _logger.LogError(ex, "Error al obtener contactos {Estado}.", estadoTexto);
                throw new Exception($"Error al obtener contactos {estadoTexto}.", ex);
            }
        }

        public async Task<Contacto> ObtenerPorIdAsync ( int id )
        {
            try
            {
                var contacto = await _context.Contactos.FindAsync(id) ?? 
                    throw new KeyNotFoundException($"No se encontró el contacto con Id {id}");

                return contacto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el contacto con ID {ContactoId}", id);
                throw new Exception("Error inesperado al obtener el contacto.", ex);
            }
        }

        public async Task<IEnumerable<Contacto>> ObtenerPorNombreAsync ( string nombre )
        {
            try
            {
                return await _context.Contactos
                    .AsNoTracking()
                    .Include(c => c.Proveedor)
                    .Where(c => c.Nombre.ToLower().Contains(nombre.ToLower()) && c.Estado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el contacto con nombre {Nombre}", nombre);
                throw new Exception("Error inesperado al obtener el contacto.", ex);
            }
        }

        public async Task<bool> AgregarAsync ( Contacto contact )
        {
            try
            {
                await _context.Contactos.AddAsync(contact);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al agregar el contacto a la base de datos.");
                throw new Exception("Error al agregar el contacto a la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar el contacto.");
                throw new Exception("Error inesperado al agregar el contacto", ex);
            }
        }

        public async Task<bool> ActualizarAsync ( Contacto contacto )
        {
            var contactoExiste = await _context.Contactos.FindAsync(contacto.ContactoId) ?? 
                throw new KeyNotFoundException($"No se encontró el contacto con el ID {contacto.ContactoId}");
            
            try
            {
                contactoExiste.Nombre = contacto.Nombre;
                contactoExiste.Descripcion = contacto.Descripcion;
                contactoExiste.Correo = contacto.Correo;
                contactoExiste.FechaModificacion = DateTime.UtcNow;
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

        public async Task<bool> CambiarEstadoAsync ( int id, bool nuevoEstado )
        {
            var contacto = await _context.Contactos.FindAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró el contacto con ID {id}");

            try
            {
                contacto.Estado = nuevoEstado;
                contacto.FechaModificacion = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                var accion = nuevoEstado ? "reactivar" : "eliminar";
                _logger.LogError(dbEx, "Error al {Accion} el contacto con ID {ContactoId}", accion, id);
                throw new Exception($"Error al {accion} el contacto en la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                var accion = nuevoEstado ? "reactivar" : "eliminar";
                _logger.LogError(ex, "Error inesperado al {Accion} el contacto con ID {ContactoId}", accion, id);
                throw new Exception($"Error inesperado al {accion} el contacto.", ex);
            }
        }

    }
}
