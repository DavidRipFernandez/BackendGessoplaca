using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly ICategoriaMaterialRepository _categoriaRepository;

        public MaterialService ( IMaterialRepository materialRepository, ICategoriaMaterialRepository categoriaRepository )
        {
            _materialRepository = materialRepository;
            _categoriaRepository = categoriaRepository;
        }

        private static MaterialDto MapearADto ( Material m ) => new()
        {
            MaterialId = m.MaterialId,
            CodigoMaterial = m.CodigoMaterial,
            Nombre = m.Nombre,
            SistemaMedicion = m.SistemaMedicion,
            Estado = m.Estado,
            CategoriaId = m.CategoriaId
        };

        public async Task<ApiResponse<List<MaterialDto>>> ObtenerActivosAsync ()
        {
            try
            {
                var materiales = await _materialRepository.ObtenerPorEstadoAsync(true);
                var listaMateriales = materiales.Select(MapearADto).ToList();

                return ApiResponse<List<MaterialDto>>.SuccessResponse(listaMateriales, "Materiales activos obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<MaterialDto>>.ErrorResponse("Error al obtener materiales activos.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<List<MaterialDto>>> ObtenerEliminadosAsync ()
        {
            try
            {
                var materiales = await _materialRepository.ObtenerPorEstadoAsync(false);
                var listaMateriales = materiales.Select(MapearADto).ToList();

                return ApiResponse<List<MaterialDto>>.SuccessResponse(listaMateriales, "Materiales eliminados obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<MaterialDto>>.ErrorResponse("Error al obtener materiales eliminados.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<MaterialDto>> ObtenerPorIdConCategoriaAsync ( int id )
        {
            try
            {
                var material = await _materialRepository.ObtenerPorIdConCategoriaAsync(id);

                if (material is null)
                {
                    return ApiResponse<MaterialDto>.ErrorResponse("Material no encontrado.", $"No se encontró el material con ID {id}.", 404);
                }

                var materialConCategoria = new MaterialDto
                {
                    MaterialId = material.MaterialId,
                    CodigoMaterial = material.CodigoMaterial,
                    Nombre = material.Nombre,
                    SistemaMedicion = material.SistemaMedicion,
                    Estado = material.Estado,
                    CategoriaId = material.CategoriaId,
                    Categoria = material.CategoriasMaterial is null ? null : new CategoriaMaterialDto
                    {
                        CategoriaId = material.CategoriasMaterial.CategoriaId,
                        Nombre = material.CategoriasMaterial.Nombre,
                        Descripcion = material.CategoriasMaterial.Descripcion
                    }
                };

                return ApiResponse<MaterialDto>.SuccessResponse(materialConCategoria, "Material obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<MaterialDto>.ErrorResponse("Error al obtener el material.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> AgregarAsync ( MaterialDto material )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(material);

                var categoria = await _categoriaRepository.ObtenerPorIdAsync(material.CategoriaId);
                if (categoria is null)
                {
                    return ApiResponse<bool>.ErrorResponse("Categoría inválida.", $"No existe la categoría con ID {material.CategoriaId}.", 400);
                }

                var duplicado = await _materialRepository.BuscarPorNombreAsync(material.Nombre);
                if (duplicado is not null)
                {
                    return ApiResponse<bool>.ErrorResponse("Nombre duplicado.", $"Ya existe un material con el nombre '{material.Nombre}'.", 409);
                }

                var nuevoMaterial = new Material
                {
                    CodigoMaterial = material.CodigoMaterial,
                    Nombre = material.Nombre,
                    SistemaMedicion = material.SistemaMedicion,
                    CategoriaId = material.CategoriaId
                };

                var agregado = await _materialRepository.AgregarAsync(nuevoMaterial);

                return agregado
                    ? ApiResponse<bool>.SuccessResponse(true, "Material agregado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo agregar el material.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al agregar material.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync ( MaterialDto material )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(material);

                if (material.MaterialId == 0)
                {
                    return ApiResponse<bool>.ErrorResponse("ID requerido.", "Falta el ID para actualizar.", 400);
                }

                var categoria = await _categoriaRepository.ObtenerPorIdAsync(material.CategoriaId);
                if (categoria is null)
                {
                    return ApiResponse<bool>.ErrorResponse("Categoría inválida.", $"No existe la categoría con ID {material.CategoriaId}.", 400);
                }

                var duplicado = await _materialRepository.BuscarPorNombreAsync(material.Nombre);
                if (duplicado is not null && duplicado.MaterialId != material.MaterialId)
                {
                    return ApiResponse<bool>.ErrorResponse("Nombre duplicado.", $"Ya existe otro material con el nombre '{material.Nombre}'.", 409);
                }

                var materialActualizado = new Material
                {
                    MaterialId = material.MaterialId,
                    CodigoMaterial = material.CodigoMaterial,
                    Nombre = material.Nombre,
                    SistemaMedicion = material.SistemaMedicion,
                    CategoriaId = material.CategoriaId
                };

                var actualizado = await _materialRepository.ActualizarAsync(materialActualizado);

                return actualizado
                    ? ApiResponse<bool>.SuccessResponse(true, "Material actualizado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo actualizar el material.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al actualizar material.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync ( int id )
        {
            try
            {
                var eliminado = await _materialRepository.CambiarEstadoAsync(id, false);

                return eliminado
                    ? ApiResponse<bool>.SuccessResponse(true, "Material eliminado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo eliminar el material.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al eliminar material.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ReactivarAsync ( int id )
        {
            try
            {
                var reactivado = await _materialRepository.CambiarEstadoAsync(id, true);

                return reactivado
                    ? ApiResponse<bool>.SuccessResponse(true, "Material reactivado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo reactivar el material.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al reactivar material.", ex.Message, 500);
            }
        }
    }
}
