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

        public async Task<bool> ActualizarAsync(Material material)
        {
            var materialExistente = await _context.Materiales.FindAsync(material.MaterialId);
            if (materialExistente != null)
                throw new KeyNotFoundException($"No se encontro el material con el ID {material.MaterialId}");

            try
            {
                materialExistente.Nombre = material.Nombre;
                materialExistente.SistemaMedicion = material.SistemaMedicion;
                materialExistente.CodigoMaterial = material.CodigoMaterial;
                materialExistente.CategoriaId = material.CategoriaId;
                materialExistente.FechaModificacion = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al actualizar el material con ID {material.materialId}", material.MaterialId);
                throw new Exception("Error al actualizar el material con ID {material.materialId}", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el material con ID {material.materialId}", material.MaterialId);
                throw new Exception("Error inesperado al actualizar el material con ID {material.materialId}",ex);
            }
        }

        public async Task<bool> AgregarAsync(Material material)
        {
            try
            {
                await _context.Materiales.AddAsync(material);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al agregar material a la base de datos");
                throw new Exception("Error al agregar material a la base de datos", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al agregar el material");
                throw new Exception("Error inesperado al agregar el material", ex);
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var materialExistente = await _context.Materiales.FindAsync(id);
            if (materialExistente != null)
                throw new KeyNotFoundException($"No se encontro el material con el ID {id}");

            try
            {
                materialExistente.Estado = false;
                materialExistente.FechaModificacion = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al Eliminar el material con ID {material.materialId}", id);
                throw new Exception("Error al Eliminar el material con ID {material.materialId}", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al Eliminar el material con ID {material.materialId}", id);
                throw new Exception("Error inesperado al Eliminar el material con ID {material.materialId}", ex);
            }
        }

        public async Task<Material> ObtenerPorIdAsync(int id)
        {
            try
            {
                var material = await _context.Materiales.FindAsync(id) ?? throw new KeyNotFoundException($"No se encontro el material con Id {id}");
                return material;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el material con Id {id}", id);
                throw new Exception("Error inesperado al obtener el material con Id {id}", ex);
            }
        }

        public async Task<IEnumerable<Material>> ObtenerTodosAsync()
        {
            try
            {
                return await _context.Materiales.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los materiales");
                throw new Exception("Error inesperado al obtener los materiales", ex);
            }
        }
    }
}
