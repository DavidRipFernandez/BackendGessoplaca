using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class ProveedorMarcaService : IProveedorMarcaService
    {
        private readonly IProveedorMarcaRepository _proveedorMarcaRepositorio;

        public ProveedorMarcaService ( IProveedorMarcaRepository proveedorMarcaRepositorio )
        {
            _proveedorMarcaRepositorio = proveedorMarcaRepositorio;
        }

        public async Task<ApiResponse<bool>> AgregarAsync ( ProveedorMarcaDto relacion )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(relacion);

                var existente = await _proveedorMarcaRepositorio.ObtenerPorIdAsync(relacion.ProveedorCifId, relacion.MarcaId);

                if (existente is not null && existente.Estado)
                {
                    return ApiResponse<bool>.ErrorResponse(
                        "La relación ya existe.",
                        $"El proveedor '{relacion.ProveedorCifId}' ya tiene asociada la marca '{relacion.MarcaId}'.",
                        409);
                }

                if (existente is not null && !existente.Estado)
                {
                    var reactivado = await _proveedorMarcaRepositorio.CambiarEstadoAsync(relacion.ProveedorCifId, relacion.MarcaId, true);
                    return reactivado
                        ? ApiResponse<bool>.SuccessResponse(true, "La relación fue reactivada correctamente.")
                        : ApiResponse<bool>.ErrorResponse("No se pudo reactivar la relación.", "Fallo en el repositorio.", 500);
                }

                var nueva = new ProveedorMarca
                {
                    ProveedorCifId = relacion.ProveedorCifId,
                    MarcaId = relacion.MarcaId
                };

                var resultado = await _proveedorMarcaRepositorio.AgregarAsync(nueva);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Relación proveedor-marca agregada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo agregar la relación.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al agregar la relación proveedor-marca.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync ( string proveedorCifId, int marcaId )
        {
            try
            {
                var resultado = await _proveedorMarcaRepositorio.CambiarEstadoAsync(proveedorCifId, marcaId, false);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Relación eliminada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo eliminar la relación.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al eliminar la relación.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ReactivarAsync ( string proveedorCifId, int marcaId )
        {
            try
            {
                var resultado = await _proveedorMarcaRepositorio.CambiarEstadoAsync(proveedorCifId, marcaId, true);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Relación reactivada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo reactivar la relación.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al reactivar la relación.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<ProveedorConMarcasDto>> ObtenerMarcasPorProveedorAsync ( string proveedorCifId )
        {
            try
            {
                var relaciones = await _proveedorMarcaRepositorio.ObtenerMarcasPorProveedorAsync(proveedorCifId);

                if (!relaciones.Any())
                {
                    return ApiResponse<ProveedorConMarcasDto>.ErrorResponse(
                        "Proveedor sin marcas.",
                        "No se encontraron marcas asociadas al proveedor.", 404);
                }

                var proveedor = relaciones.First().Proveedor;

                var proveedorConMarcas = new ProveedorConMarcasDto
                {
                    ProveedorCifId = proveedor.ProveedorCifId,
                    Nombre = proveedor.Nombre,
                    DomicilioSocial = proveedor.DomicilioSocial,
                    Marcas = relaciones.Select(pm => new MarcaDto
                    {
                        MarcaId = pm.Marca.MarcaId,
                        Nombre = pm.Marca.Nombre,
                        Descripcion = pm.Marca.Descripcion
                    }).ToList()
                };

                return ApiResponse<ProveedorConMarcasDto>.SuccessResponse(proveedorConMarcas, "Marcas del proveedor obtenidas correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProveedorConMarcasDto>.ErrorResponse("Error al obtener marcas del proveedor.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<MarcaConProveedoresDto>> ObtenerProveedoresPorMarcaAsync ( int marcaId )
        {
            try
            {
                var relaciones = await _proveedorMarcaRepositorio.ObtenerProveedoresPorMarcaAsync(marcaId);

                if (!relaciones.Any())
                {
                    return ApiResponse<MarcaConProveedoresDto>.ErrorResponse(
                        "Marca sin proveedores.",
                        "No se encontraron proveedores asociados a la marca.", 404);
                }

                var marca = relaciones.First().Marca;

                var marcaConProveedores = new MarcaConProveedoresDto
                {
                    MarcaId = marca.MarcaId,
                    Nombre = marca.Nombre,
                    Descripcion = marca.Descripcion,
                    Proveedores = relaciones.Select(pm => new ProveedorDto
                    {
                        ProveedorCifId = pm.Proveedor.ProveedorCifId,
                        Nombre = pm.Proveedor.Nombre,
                        DomicilioSocial = pm.Proveedor.DomicilioSocial
                    }).ToList()
                };

                return ApiResponse<MarcaConProveedoresDto>.SuccessResponse(marcaConProveedores, "Proveedores de la marca obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<MarcaConProveedoresDto>.ErrorResponse("Error al obtener proveedores de la marca.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<List<ProveedorMarcaDto>>> ObtenerEliminadosAsync ()
        {
            try
            {
                var entidades = await _proveedorMarcaRepositorio.ObtenerEliminadosAsync();

                var resultado = entidades.Select(pm => new ProveedorMarcaDto
                {
                    ProveedorCifId = pm.ProveedorCifId,
                    MarcaId = pm.MarcaId,
                    Estado = pm.Estado
                }).ToList();

                return ApiResponse<List<ProveedorMarcaDto>>.SuccessResponse(resultado, "Relaciones eliminadas obtenidas correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ProveedorMarcaDto>>.ErrorResponse("Error al obtener relaciones eliminadas.", ex.Message, 500);
            }
        }

    }
}
