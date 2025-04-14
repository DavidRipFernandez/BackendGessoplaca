using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;
using NSoft.Repositories.IRepositories;

namespace NSoft.Repositories
{
    public class CategoriaMaterialRepository : ICategoriaMaterialRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoriaMaterialRepository> _logger;

        public CategoriaMaterialRepository ( ApplicationDbContext context, ILogger<CategoriaMaterialRepository> logger )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoriaMaterial>> ObtenerPorEstadoAsync ( bool estado )
        {
            try
            {
                return await _context.CategoriasMateriales
                    .AsNoTracking()
                    .Where(c => c.Estado == estado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener categorías por estado.");
                throw new Exception("Error al obtener las categorías.", ex);
            }
        }

        public async Task<CategoriaMaterial?> ObtenerPorIdAsync ( int id )
        {
            try
            {
                return await _context.CategoriasMateriales
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CategoriaId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la categoría con ID {CategoriaId}.", id);
                throw new Exception("Error al obtener la categoría.", ex);
            }
        }

        public async Task<CategoriaMaterial?> ObtenerPorIdConMaterialesAsync ( int id )
        {
            try
            {
                return await _context.CategoriasMateriales
                    .Include(c => c.Materiales)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CategoriaId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la categoría con materiales con ID {CategoriaId}.", id);
                throw new Exception("Error al obtener la categoría con sus materiales.", ex);
            }
        }

        public async Task<bool> AgregarAsync ( CategoriaMaterial categoria )
        {
            try
            {
                await _context.CategoriasMateriales.AddAsync(categoria);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al agregar la categoría.");
                throw new Exception("Error de base de datos al agregar la categoría.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar la categoría.");
                throw new Exception("Error inesperado al agregar la categoría.", ex);
            }
        }

        public async Task<bool> ActualizarAsync ( CategoriaMaterial categoria )
        {
            try
            {
                var existente = await _context.CategoriasMateriales.FindAsync(categoria.CategoriaId);
                if (existente == null)
                    throw new KeyNotFoundException($"No se encontró la categoría con ID {categoria.CategoriaId}");

                existente.Nombre = categoria.Nombre;
                existente.Descripcion = categoria.Descripcion;
                existente.FechaModificacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al actualizar la categoría con ID {CategoriaId}.", categoria.CategoriaId);
                throw new Exception("Error de base de datos al actualizar la categoría.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar la categoría con ID {CategoriaId}.", categoria.CategoriaId);
                throw new Exception("Error inesperado al actualizar la categoría.", ex);
            }
        }

        public async Task<bool> CambiarEstadoAsync ( int id, bool estado )
        {
            try
            {
                var categoria = await _context.CategoriasMateriales.FindAsync(id)
                                ?? throw new KeyNotFoundException($"No se encontró la categoría con ID {id}");

                categoria.Estado = estado;
                categoria.FechaModificacion = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al actualizar el estado de la categoría con ID {CategoriaId}.", id);
                throw new Exception("Error de base de datos al actualizar el estado de la categoría.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar el estado de la categoría con ID {CategoriaId}.", id);
                throw new Exception("Error al cambiar el estado de la categoría.", ex);
            }
        }
    }
}
