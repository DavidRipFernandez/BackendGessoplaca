using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;
using NSoft.Repositories.IRepositories;

namespace NSoft.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MarcaRepository> _logger;

        public MarcaRepository (ApplicationDbContext context, ILogger<MarcaRepository> logger )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> ActualizarAsync ( Marca marca )
        {
            var marcaExistente = await _context.Marcas.FindAsync(marca.MarcaId);
            if (marcaExistente == null)
                throw new KeyNotFoundException($"No se encontro la marca con el ID {marca.MarcaId}");

            try
            {
                marcaExistente.FechaModificacion = DateTime.UtcNow;
                marcaExistente.Nombre = marca.Nombre;
                marcaExistente.Descripcion = marca.Descripcion;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al actualizar la marca con ID {marca.MarcaId}", marca.MarcaId);
                throw new Exception("Error al actualizar la marca en la base de datos", dbEx);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error inesperado al actualizar la marca con ID {marca.MarcaId}", marca.MarcaId);
                throw new Exception("Error inesperado al actualizar la marca.", ex);
            }
        }

        public async Task<bool> AgregarAsync ( Marca marca )
        {
            try
            {
                await _context.Marcas.AddAsync(marca);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al agregar la marca a la base de datos.");
                throw new Exception("Error al agregar la marca a la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar la marca.");
                throw new Exception("Error inesperado al agregar la marca.", ex);
            }
        }

        public async Task<bool> EliminarAsync ( int id )
        {
            try
            {
                var marca = await _context.Marcas.FindAsync(id) ?? throw new KeyNotFoundException($"No se encontro la marca con ID {id}");

                marca.FechaModificacion = DateTime.UtcNow;
                marca.Estado = false;
                _context.Marcas.Update(marca);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al eliminar la marca con ID {id}", id);
                throw new Exception("Error al eliminar la marca en la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar la marca con ID {id}", id);
                throw new Exception("Error inesperado al eliminar la marca.", ex);
            }
        }

        public async Task<Marca> ObtenerPorIdAsync ( int id )
        {
            try
            {
                var marca = await _context.Marcas.FindAsync(id) ?? throw new KeyNotFoundException($"No se encontro la marca con ID {id}");
                return marca;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener la marca con ID {id}", id);
                throw new Exception("Error inesperado al obtener la marca.", ex);
            }
        }

        public async Task<IEnumerable<Marca>> ObtenerActivosAsync ()
        {
            try
            {
                return await _context.Marcas.AsNoTracking().Where(m => m.Estado == true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener las marcas.");
                throw new Exception("Error inesperado al obtener las marcas.", ex);
            }
        }

        public async Task<IEnumerable<Marca>> ObtenerEliminadosAsync ()
        {
            try
            {
                return await _context.Marcas.AsNoTracking().Where(m => m.Estado == false).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener las marcas.");
                throw new Exception("Error inesperado al obtener las marcas.", ex);
            }
        }

        public async Task<Marca?> ObtenerPorIdConProveedoresAsync ( int id )
        {
            try
            {
                return await _context.Marcas
                    .Include(m => m.ProveedoresMarcas)
                    .ThenInclude(pm => pm.Proveedor)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.MarcaId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener la marca con proveedores.");
                throw new Exception("Error inesperado al obtener las marcas con proveedores.", ex);
            }
        }
    }
}
