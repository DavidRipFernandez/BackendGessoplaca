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
            catch (Exception ex)
            {
                return ApiResponse<List<CategoriaMaterialDto>>.ErrorResponse("Error al obtener categorías activas.", ex.Message, 500);
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
            catch (Exception ex)
            {
                return ApiResponse<List<CategoriaMaterialDto>>.ErrorResponse("Error al obtener categorías eliminadas.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<CategoriaMaterialDto>> ObtenerPorIdAsync ( int id )
        {
            try
            {
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
            catch (Exception ex)
            {
                return ApiResponse<CategoriaMaterialDto>.ErrorResponse("Error al obtener la categoría.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<CategoriaMaterialDto>> ObtenerPorIdConMaterialesAsync ( int id )
        {
            try
            {
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
            catch (Exception ex)
            {
                return ApiResponse<CategoriaMaterialDto>.ErrorResponse("Error al obtener la categoría con materiales.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> AgregarAsync ( CategoriaMaterialDto categoriaDto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(categoriaDto);

                var nuevaCategoria = new CategoriaMaterial
                {
                    Nombre = categoriaDto.Nombre,
                    Descripcion = categoriaDto.Descripcion
                };

                var resultado = await _categoriaRepository.AgregarAsync(nuevaCategoria);
                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Categoría agregada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo agregar la categoría.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al agregar categoría.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync ( CategoriaMaterialDto categoriaDto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(categoriaDto);

                var categoriaActualizada = new CategoriaMaterial
                {
                    CategoriaId = categoriaDto.CategoriaId,
                    Nombre = categoriaDto.Nombre,
                    Descripcion = categoriaDto.Descripcion
                };

                var resultado = await _categoriaRepository.ActualizarAsync(categoriaActualizada);
                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Categoría actualizada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo actualizar la categoría.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al actualizar categoría.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync ( int id )
        {
            try
            {
                var resultado = await _categoriaRepository.CambiarEstadoAsync(id, false);
                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Categoría eliminada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo eliminar la categoría.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al eliminar categoría.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ReactivarAsync ( int id )
        {
            try
            {
                var resultado = await _categoriaRepository.CambiarEstadoAsync(id, true);
                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Categoría reactivada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo reactivar la categoría.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al reactivar categoría.", ex.Message, 500);
            }
        }
    }
}
