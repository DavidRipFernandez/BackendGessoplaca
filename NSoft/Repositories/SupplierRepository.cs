using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using System.Text.RegularExpressions;

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

        public async Task<IEnumerable<Proveedor>> ObtenerPorEstadoAsync ( bool estado )
        {
            try
            {
                return await _context.Proveedores
                    .AsNoTracking()
                    .Where(p => p.Estado == estado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                var estadoTexto = estado ? "activos" : "eliminados";
                _logger.LogError(ex, "Error al obtener proveedores {Estado}.", estadoTexto);
                throw new Exception($"Error al obtener proveedores {estadoTexto}.", ex);
            }
        }

        public async Task<Proveedor?> ObtenerPorIdAsync(string id)
        {
            try
            {
                var proveedor = await _context.Proveedores.FindAsync(id)
                            ?? throw new KeyNotFoundException($"No se encontró el proveedor con ID {id}");
                return proveedor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el proveedor con ID {id}.", id);
                throw new Exception("Error al obtener el proveedor.", ex);
            }
        }

        public async Task<Proveedor?> ObtenerProveedorConRelacionesAsync ( string id )
        {
            try
            {
                var supplier = await _context.Proveedores
                    .Include(p => p.Contactos)
                    .Include(p => p.ProveedoresMarcas)
                    .ThenInclude(pm => pm.Marca)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.ProveedorCifId == id) ??

                    throw new KeyNotFoundException($"No se encontró al proveedor con Id {id}");

                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener proveedor con relaciones para el ID {ProveedorCifId}", id);
                throw new Exception("Error inesperado al obtener proveedor con relaciones.", ex);
            }
        }

        public async Task<bool> AgregarAsync ( Proveedor supplier )
        {
            try
            {
                await _context.Proveedores.AddAsync(supplier);
                await _context.SaveChangesAsync();
                return true;
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

        public async Task<bool> ActualizarAsync ( Proveedor proveedor )
        {
            var proveedorExiste = await _context.Proveedores.FindAsync(proveedor.ProveedorCifId);
            if (proveedorExiste == null)
                throw new KeyNotFoundException($"No se encontró el proveedor con el ID {proveedor.ProveedorCifId}");

            try
            {
                proveedorExiste.Nombre = proveedor.Nombre;
                proveedorExiste.DomicilioSocial = proveedor.DomicilioSocial;
                proveedorExiste.FechaModificacion = DateTime.UtcNow;
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

        public async Task<bool> CambiarEstadoAsync ( string id, bool estado )
        {
            try
            {
                var proveedor = await _context.Proveedores.FindAsync(id);
                if (proveedor == null)
                    throw new KeyNotFoundException($"No se encontró el proveedor con el ID {id}");

                proveedor.Estado = estado;
                proveedor.FechaModificacion = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al cambiar el estado del proveedor con ID {ProveedorCifId}", id);
                throw new Exception("Error al cambiar el estado del proveedor en la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al cambiar el estado del proveedor con ID {ProveedorCifId}", id);
                throw new Exception("Error inesperado al cambiar el estado del proveedor.", ex);
            }
        }
    }
}
