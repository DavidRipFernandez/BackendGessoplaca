using Microsoft.EntityFrameworkCore; 
using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class CategoriaMaterialService : ICategoriaMaterialService
    {
        private readonly ICategoriaMaterialRepository _categoriaRepository;

        public CategoriaMaterialService ( ICategoriaMaterialRepository categoriaRepository )
        {
            _categoriaRepository = categoriaRepository;
        }


        private static ApiResponse<T> FromException<T> ( string userMessage, Exception ex ) // ADDED
        {
            // REGLA DEV: incluir ex.Message
            return ex switch
            {
                KeyNotFoundException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 404),
                DbUpdateConcurrencyException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 409),
                DbUpdateException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 409),
                ArgumentException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 400),
                _ => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 500)
            };
        }

        public async Task<ApiResponse<List<CategoriaMaterialDto>>> ObtenerActivosAsync ()
        {
            try
            {
                var categorias = await _categoriaRepository.ObtenerPorEstadoAsync(true);
                var resultado = categorias.Select(c => new CategoriaMaterialDto
                {
                    CategoriaId = c.CategoriaId,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    Estado = c.Estado
                }).ToList();

                return ApiResponse<List<CategoriaMaterialDto>>.SuccessResponse(resultado, "Categorías activas obtenidas correctamente.");
            }
            catch (ArgumentException ex) 
            {
                return FromException<List<CategoriaMaterialDto>>("Error de validación al obtener categorías activas.", ex); 
            }
            catch (DbUpdateException ex) 
            {
                return FromException<List<CategoriaMaterialDto>>("Error de base de datos al obtener categorías activas.", ex);
            }
            catch (Exception ex) 
            {
                return FromException<List<CategoriaMaterialDto>>("Error al obtener categorías activas.", ex);
            }
        }

        public async Task<ApiResponse<List<CategoriaMaterialDto>>> ObtenerEliminadosAsync ()
        {
            try
            {
                var categorias = await _categoriaRepository.ObtenerPorEstadoAsync(false);
                var resultado = categorias.Select(c => new CategoriaMaterialDto
                {
                    CategoriaId = c.CategoriaId,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    Estado = c.Estado
                }).ToList();

                return ApiResponse<List<CategoriaMaterialDto>>.SuccessResponse(resultado, "Categorías eliminadas obtenidas correctamente.");
            }
            catch (ArgumentException ex)
            {
                return FromException<List<CategoriaMaterialDto>>("Error de validación al obtener categorías eliminadas.", ex);
            }
            catch (DbUpdateException ex)
            {
                return FromException<List<CategoriaMaterialDto>>("Error de base de datos al obtener categorías eliminadas.", ex);
            }
            catch (Exception ex)
            {
                return FromException<List<CategoriaMaterialDto>>("Error al obtener categorías eliminadas.", ex);
            }
        }

        public async Task<ApiResponse<CategoriaMaterialDto>> ObtenerPorIdAsync ( int id )
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");

                var categoria = await _categoriaRepository.ObtenerPorIdAsync(id);
                if (categoria == null)
                    return ApiResponse<CategoriaMaterialDto>.ErrorResponse("Categoría no encontrada.", $"No existe una categoría con ID {id}.", 404);

                var resultado = new CategoriaMaterialDto
                {
                    CategoriaId = categoria.CategoriaId,
                    Nombre = categoria.Nombre,
                    Descripcion = categoria.Descripcion,
                    Estado = categoria.Estado
                };

                return ApiResponse<CategoriaMaterialDto>.SuccessResponse(resultado, "Categoría obtenida correctamente.");
            }
            catch (ArgumentException ex)
            {
                return FromException<CategoriaMaterialDto>("Error de validación al obtener la categoría.", ex);
            }
            catch (DbUpdateException ex)
            {
                return FromException<CategoriaMaterialDto>("Error de base de datos al obtener la categoría.", ex);
            }
            catch (Exception ex)
            {
                return FromException<CategoriaMaterialDto>("Error al obtener la categoría.", ex);
            }
        }

        public async Task<ApiResponse<CategoriaMaterialDto>> ObtenerPorIdConMaterialesAsync ( int id )
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");

                var categoria = await _categoriaRepository.ObtenerPorIdConMaterialesAsync(id);
                if (categoria == null)
                    return ApiResponse<CategoriaMaterialDto>.ErrorResponse("Categoría no encontrada.", $"No existe una categoría con ID {id}.", 404);

                var resultado = new CategoriaMaterialDto
                {
                    CategoriaId = categoria.CategoriaId,
                    Nombre = categoria.Nombre,
                    Descripcion = categoria.Descripcion,
                    Estado = categoria.Estado,
                    Materiales = categoria.Materiales.Select(m => new MaterialDto
                    {
                        MaterialId = m.MaterialId,
                        CodigoMaterial = m.CodigoMaterial,
                        Nombre = m.Nombre,
                        SistemaMedicion = m.SistemaMedicion,
                        Estado = m.Estado
                    }).ToList()
                };

                return ApiResponse<CategoriaMaterialDto>.SuccessResponse(resultado, "Categoría con materiales obtenida correctamente.");
            }
            catch (ArgumentException ex)
            {
                return FromException<CategoriaMaterialDto>("Error de validación al obtener la categoría con materiales.", ex);
            }
            catch (DbUpdateException ex)
            {
                return FromException<CategoriaMaterialDto>("Error de base de datos al obtener la categoría con materiales.", ex);
            }
            catch (Exception ex)
            {
                return FromException<CategoriaMaterialDto>("Error al obtener la categoría con materiales.", ex);
            }
        }

        public async Task<ApiResponse<bool>> AgregarAsync ( CategoriaMaterialDto categoriaDto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(categoriaDto);

                if (string.IsNullOrWhiteSpace(categoriaDto.Nombre))
                    throw new ArgumentException("El nombre de la categoría es obligatorio.");

                var nuevaCategoria = new CategoriaMaterial
                {
                    Nombre = categoriaDto.Nombre,
                    Descripcion = categoriaDto.Descripcion
                };

                await _categoriaRepository.AgregarAsync(nuevaCategoria);
                return ApiResponse<bool>.SuccessResponse(true, "Categoría agregada correctamente.");
            }
            catch (ArgumentException ex)
            {
                return FromException<bool>("Error de validación al agregar la categoría.", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return FromException<bool>("Conflicto de concurrencia al agregar la categoría.", ex);
            }
            catch (DbUpdateException ex)
            {
                return FromException<bool>("Error de base de datos al agregar la categoría.", ex);
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al agregar categoría.", ex);
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync ( CategoriaMaterialDto categoriaDto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(categoriaDto);

                if (categoriaDto.CategoriaId <= 0)
                    throw new ArgumentException("El ID de la categoría es obligatorio y debe ser válido.");

                if (string.IsNullOrWhiteSpace(categoriaDto.Nombre))
                    throw new ArgumentException("El nombre de la categoría es obligatorio.");

                var categoriaActualizada = new CategoriaMaterial
                {
                    CategoriaId = categoriaDto.CategoriaId,
                    Nombre = categoriaDto.Nombre,
                    Descripcion = categoriaDto.Descripcion
                };

                await _categoriaRepository.ActualizarAsync(categoriaActualizada);
                return ApiResponse<bool>.SuccessResponse(true, "Categoría actualizada correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return FromException<bool>("No se pudo actualizar la categoría.", ex);
            }
            catch (ArgumentException ex) // ADDED
            {
                return FromException<bool>("Error de validación al actualizar la categoría.", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return FromException<bool>("Conflicto de concurrencia al actualizar la categoría.", ex);
            }
            catch (DbUpdateException ex)
            {
                return FromException<bool>("Error de base de datos al actualizar la categoría.", ex);
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al actualizar categoría.", ex);
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync ( int id )
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");

                await _categoriaRepository.CambiarEstadoAsync(id, false);
                return ApiResponse<bool>.SuccessResponse(true, "Categoría eliminada correctamente.");
            }
            catch (KeyNotFoundException ex) 
            {
                return FromException<bool>("No se pudo eliminar la categoría.", ex);
            }
            catch (ArgumentException ex)
            {
                return FromException<bool>("Error de validación al eliminar la categoría.", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return FromException<bool>("Conflicto de concurrencia al eliminar la categoría.", ex);
            }
            catch (DbUpdateException ex)
            {
                return FromException<bool>("Error de base de datos al eliminar la categoría.", ex);
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al eliminar categoría.", ex);
            }
        }

        public async Task<ApiResponse<bool>> ReactivarAsync ( int id )
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");

                await _categoriaRepository.CambiarEstadoAsync(id, true);
                return ApiResponse<bool>.SuccessResponse(true, "Categoría reactivada correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return FromException<bool>("No se pudo reactivar la categoría.", ex);
            }
            catch (ArgumentException ex)
            {
                return FromException<bool>("Error de validación al reactivar la categoría.", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return FromException<bool>("Conflicto de concurrencia al reactivar la categoría.", ex);
            }
            catch (DbUpdateException ex)
            {
                return FromException<bool>("Error de base de datos al reactivar la categoría.", ex);
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al reactivar categoría.", ex);
            }
        }
    }
}
