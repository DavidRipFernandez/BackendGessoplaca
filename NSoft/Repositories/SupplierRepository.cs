using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;

namespace NSoft.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SupplierRepository> _logger;

        public SupplierRepository ( ApplicationDbContext context, ILogger<SupplierRepository> logger )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> ActualizarAsync ( Proveedor proveedor )
        {
            try
            {
                var existe = await _context.Proveedores.AnyAsync(supplier => supplier.ProveedorCifId == proveedor.ProveedorCifId);
                if (!existe)
                    throw new KeyNotFoundException($"No se encontró el proveedor con el ID {proveedor.ProveedorCifId}");

                _context.Proveedores.Update(proveedor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al actualizar el proveedor con ID {ProveedorCifId}", proveedor.ProveedorCifId);
                throw new Exception("Error al actualizar el proveedor en la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el proveedor con ID {ProveedorCifId}", proveedor.ProveedorCifId);
                throw new Exception("Error inesperado al actualizar el proveedor.", ex);
            }
        }

        public async Task AgregarAsync ( Proveedor supplier )
        {
            try
            {
                await _context.Proveedores.AddAsync(supplier);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al agregar al proveedor a la base de datos.");
                throw new Exception("Error al agregar al proveedor a la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar al proveedor.");
                throw new Exception("Error inesperado al agregar al proveedor", ex);
            }
        }

        public async Task<bool> EliminarAsync ( string id )
        {
            try
            {
                var supplier = await _context.Proveedores.FindAsync(id) ?? throw new KeyNotFoundException($"No se encontro al proveedor con ID {id}");

                _context.Proveedores.Remove(supplier);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al eliminar al proveedor con ID {ProveedorCifId}", id);
                throw new Exception("Error al eliminar al proveedor en la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar al proveedor con ID {ProveedorCifId}", id);
                throw new Exception("Error inesperado al eliminar al proveedor.", ex);
            }
        }

        public async Task<Proveedor> ObtenerPorIdAsync ( string id )
        {
            try
            {
                var supplier = await _context.Proveedores.Include(p => p.Contactos).FirstOrDefaultAsync(p => p.ProveedorCifId == id) ?? throw new KeyNotFoundException($"No se encontró al proveedor con Id {id}");

                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener al proveedor con ID {ProveedorCifId}", id);
                throw new Exception("Error inesperado al obtener al proveedor.", ex);
            }
        }

        public async Task<IEnumerable<Proveedor>> ObtenerTodosAsync ()
        {
            try
            {
                return await _context.Proveedores.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los proveedores.");
                throw new Exception("Error inesperado al obtener los proveedores.", ex);
            }
        }
    }
}
