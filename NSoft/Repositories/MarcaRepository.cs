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

        public async Task<IEnumerable<Marca>> ObtenerPorEstadoAsync ( bool estado )
        {
            try
            {
                return await _context.Marcas
                    .AsNoTracking()
                    .Where(m => m.Estado == estado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener marcas por estado ({estado}).", estado);
                throw new Exception("Error al obtener las marcas.", ex);
            }
        }

        public async Task<Marca> ObtenerPorIdAsync ( int id )
        {
            try
            {
                var marca = await _context.Marcas.FindAsync(id)
                            ?? throw new KeyNotFoundException($"No se encontró la marca con ID {id}");
                return marca;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la marca con ID {id}.", id);
                throw new Exception("Error al obtener la marca.", ex);
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
                _logger.LogError(dbEx, "Error al agregar la marca.");
                throw new Exception("Error al agregar la marca a la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar la marca.");
                throw new Exception("Error inesperado al agregar la marca.", ex);
            }
        }

        public async Task<bool> ActualizarAsync ( Marca marca )
        {
            try
            {
                var marcaExistente = await _context.Marcas.FindAsync(marca.MarcaId);
                if (marcaExistente == null)
                    throw new KeyNotFoundException($"No se encontró la marca con ID {marca.MarcaId}");

                marcaExistente.Nombre = marca.Nombre;
                marcaExistente.Descripcion = marca.Descripcion;
                marcaExistente.FechaModificacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al actualizar la marca con ID {id}", marca.MarcaId);
                throw new Exception("Error al actualizar la marca en la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar la marca con ID {id}", marca.MarcaId);
                throw new Exception("Error inesperado al actualizar la marca.", ex);
            }
        }

        public async Task<bool> CambiarEstadoAsync ( int id, bool estado )
        {
            try
            {
                var marca = await _context.Marcas.FindAsync(id)
                            ?? throw new KeyNotFoundException($"No se encontró la marca con ID {id}");

                marca.Estado = estado;
                marca.FechaModificacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al cambiar el estado de la marca con ID {id}", id);
                throw new Exception("Error al cambiar el estado de la marca.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al cambiar el estado de la marca con ID {id}", id);
                throw new Exception("Error inesperado al cambiar el estado de la marca.", ex);
            }
        }

<<<<<<< HEAD
        public async Task<Marca?> ObtenerPorNombreAsync ( string nombre )
        {
            return await _context.Marcas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Nombre.ToLower() == nombre.ToLower());

        }
=======
        public async Task<Marca?> BuscarPorNombreAsync(string nombre)
        {
            try
            {
                return await _context.Marcas
                    .AsNoTracking() //desactiva el tacking de EF Ymejora el rendimiento para que no rastree cambios en la entidad, util para consultas de solo lectura
                    .FirstOrDefaultAsync(m => m.Nombre.ToLower() == nombre.ToLower()); //Retorna el primer elemento que cumpla la condicion, retorna null si no cumple
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al buscar marca por nombre {nombre}");
                throw new Exception("Error al buscar marca por nombre.", ex);
            }
        }

        //public async Task<Marca?> ObtenerPorIdConProveedoresAsync ( int id )
        //{
        //    try
        //    {
        //        return await _context.Marcas
        //            .Include(m => m.ProveedoresMarcas)
        //            .ThenInclude(pm => pm.Proveedor)
        //            .AsNoTracking()
        //            .FirstOrDefaultAsync(m => m.MarcaId == id);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error inesperado al obtener la marca con proveedores.");
        //        throw new Exception("Error inesperado al obtener las marcas con proveedores.", ex);
        //    }
        //}
>>>>>>> origin/feature/PrecioMaterialesProveedor
    }
}
