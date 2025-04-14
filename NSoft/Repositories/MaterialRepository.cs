using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using System.Collections.Generic;
using System.Data.Common;

namespace NSoft.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MaterialRepository> _logger;

        public MaterialRepository(ApplicationDbContext context, ILogger<MaterialRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Material>> ObtenerPorEstadoAsync ( bool estado )
        {
            try
            {
                return await _context.Materiales
                    //.Include(m => m.Categoria) 
                    .AsNoTracking()
                    .Where(m => m.Estado == estado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener materiales por estado.");
                throw new Exception("Error al obtener materiales.", ex);
            }
        }

        public async Task<Material?> ObtenerPorIdConCategoriaAsync ( int id )
        {
            try
            {
                return await _context.Materiales
                    .Include(m => m.CategoriasMaterial)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.MaterialId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener material con categoría para el ID {id}.", id);
                throw new Exception("Error al obtener material con categoría.", ex);
            }
        }

        public async Task<Material?> BuscarPorNombreAsync ( string nombre )
        {
            try
            {
                return await _context.Materiales
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Nombre.ToLower() == nombre.ToLower());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar material por nombre {Nombre}.", nombre);
                throw new Exception("Error al buscar material por nombre.", ex);
            }
        }

        public async Task<bool> AgregarAsync ( Material material )
        {
            try
            {
                await _context.Materiales.AddAsync(material);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de base de datos al agregar el material con código {Codigo}.", material.CodigoMaterial);
                throw new Exception("Error al guardar el material. Revisa si el código o la categoría ya existen o son inválidos.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar el material.");
                throw new Exception("Error inesperado al agregar el material.", ex);
            }
        }

        public async Task<bool> ActualizarAsync ( Material material )
        {
            try
            {
                var existente = await _context.Materiales.FindAsync(material.MaterialId);
                if (existente == null)
                    throw new KeyNotFoundException($"No se encontró el material con ID {material.MaterialId}");

                existente.Nombre = material.Nombre;
                existente.CodigoMaterial = material.CodigoMaterial;
                existente.SistemaMedicion = material.SistemaMedicion;
                existente.CategoriaId = material.CategoriaId;
                existente.FechaModificacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error de base de datos al actualizar el material con ID {MaterialId}.", material.MaterialId);
                throw new Exception("Error de base de datos al actualizar el material.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el material con ID {MaterialId}.", material.MaterialId);
                throw new Exception("Error inesperado al actualizar el material.", ex);
            }
        }

        public async Task<bool> CambiarEstadoAsync ( int id, bool estado )
        {
            try
            {
                var material = await _context.Materiales.FindAsync(id)
                              ?? throw new KeyNotFoundException($"No se encontró el material con ID {id}");

                material.Estado = estado;
                material.FechaModificacion = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar el estado del material.");
                throw new Exception("Error al cambiar el estado del material.", ex);
            }
        }
    }
}
