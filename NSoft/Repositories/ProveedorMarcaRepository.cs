using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;
using NSoft.Repositories.IRepositories;

namespace NSoft.Repositories
{
    public class ProveedorMarcaRepository : IProveedorMarcaRepository 
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProveedorMarcaRepository> _logger;

        public ProveedorMarcaRepository ( ApplicationDbContext context, ILogger<ProveedorMarcaRepository> logger )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ProveedorMarca>> ObtenerEliminadosAsync ()
        {
            try
            {
                return await _context.ProveedoresMarcas
                    .Include(pm => pm.Marca)
                    .Include(pm => pm.Proveedor)
                    .Where(pm => pm.Estado == false)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener marcas por proveedor.");
                throw new Exception("Error al obtener marcas por proveedor.", ex);
            }
        }

        public async Task<IEnumerable<ProveedorMarca>> ObtenerMarcasPorProveedorAsync ( string proveedorCifId )
        {
            try
            {
                return await _context.ProveedoresMarcas
                    .Include(pm => pm.Marca)
                    .Include(pm => pm.Proveedor)
                    .Where(pm => pm.ProveedorCifId == proveedorCifId && pm.Estado)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener marcas por proveedor.");
                throw new Exception("Error al obtener marcas por proveedor.", ex);
            }
        }

        public async Task<IEnumerable<ProveedorMarca>> ObtenerProveedoresPorMarcaAsync ( int marcaId )
        {
            try
            {
                return await _context.ProveedoresMarcas
                    .Include(pm => pm.Marca)
                    .Include(pm => pm.Proveedor)
                    .Where(pm => pm.MarcaId == marcaId && pm.Estado)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener proveedores por marca.");
                throw new Exception("Error al obtener proveedores por marca.", ex);
            }
        }

        public async Task<ProveedorMarca?> ObtenerPorIdAsync ( string proveedorCifId, int marcaId )
        {
            try
            {
                return await _context.ProveedoresMarcas
                    .AsNoTracking()
                    .FirstOrDefaultAsync(pm => pm.ProveedorCifId == proveedorCifId && pm.MarcaId == marcaId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener ProveedorMarca por clave compuesta.");
                throw new Exception("Error al obtener ProveedorMarca.", ex);
            }
        }
        public async Task<bool> AgregarAsync ( ProveedorMarca entidad )
        {
            try
            {
                await _context.ProveedoresMarcas.AddAsync(entidad);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al agregar ProveedorMarca ({Proveedor}, {Marca})", entidad.ProveedorCifId, entidad.MarcaId);
                throw new Exception("Error de base de datos al agregar la relación proveedor-marca.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar ProveedorMarca.");
                throw new Exception("Error inesperado al agregar ProveedorMarca.", ex);
            }
        }

        public async Task<bool> CambiarEstadoAsync ( string proveedorCifId, int marcaId, bool estado )
        {
            try
            {
                var relacion = await _context.ProveedoresMarcas
                    .FirstOrDefaultAsync(pm => pm.ProveedorCifId == proveedorCifId && pm.MarcaId == marcaId)
                    ?? throw new KeyNotFoundException("No se encontró la relación proveedor-marca.");

                relacion.Estado = estado;
                relacion.FechaModificacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al cambiar estado de ProveedorMarca ({Proveedor}, {Marca})", proveedorCifId, marcaId);
                throw new Exception("Error de base de datos al cambiar estado de la relación proveedor-marca.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar estado de ProveedorMarca.");
                throw new Exception("Error al cambiar estado de ProveedorMarca.", ex);
            }
        }
    }
}
