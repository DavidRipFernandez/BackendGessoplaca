using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class PrecioTarifaService : IPrecioTarifaService
    {
        private readonly IPrecioTarifaRepository _precioTarifaRepository;

        public PrecioTarifaService ( IPrecioTarifaRepository precioTarifaRepository )
        {
            _precioTarifaRepository = precioTarifaRepository;
        }

        public async Task<ApiResponse<bool>> GuardarPrecioAsync ( PrecioTarifaDto dto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(dto);

                var entidad = new PrecioTarifa
                {
                    MaterialId = dto.MaterialId,
                    MarcaId = dto.MarcaId,
                    ProveedorCifId = dto.ProveedorCifId,
                    Precio = dto.Precio
                };

                var resultado = await _precioTarifaRepository.GuardarPrecioAsync(entidad);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Precio guardado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo guardar el precio.", "Error en repositorio", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al guardar el precio.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<PreciosProveedorDto>> ObtenerPreciosPorProveedorAsync ( string proveedorCifId )
        {
            try
            {
                var precios = await _precioTarifaRepository.ObtenerPreciosPorProveedorAsync(proveedorCifId);

                if (!precios.Any())
                {
                    return ApiResponse<PreciosProveedorDto>.ErrorResponse(
                        "No se encontraron precios.",
                        $"No hay precios registrados para el proveedor con CIF {proveedorCifId}.",
                        404);
                }

                var proveedor = precios.First();

                var resultado = new PreciosProveedorDto
                {
                    ProveedorCifId = proveedor.ProveedorCifId,
                    NombreProveedor = proveedor.NombreProveedor,
                    Materiales = precios.ToList()
                };

                return ApiResponse<PreciosProveedorDto>.SuccessResponse(resultado, "Precios obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<PreciosProveedorDto>.ErrorResponse("Error al obtener precios por proveedor.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<MarcaConPreciosDto>> ObtenerPrecioPorMarcaAsync ( int marcaId )
        {
            try
            {
                var resumenPrecios = await _precioTarifaRepository.ObtenerPreciosPorMarcaAsync(marcaId);

                if (!resumenPrecios.Any())
                {
                    return ApiResponse<MarcaConPreciosDto>.ErrorResponse(
                        "No se encontraron precios.",
                        $"No existen registros de precios para la marca con ID {marcaId}.", 404);
                }

                var marca = resumenPrecios.First();

                var marcaConPrecios = new MarcaConPreciosDto
                {
                    MarcaId = marca.MarcaId,
                    NombreMarca = marca.NombreMarca,
                    Materiales = resumenPrecios.ToList()
                };

                return ApiResponse<MarcaConPreciosDto>.SuccessResponse(marcaConPrecios, "Precios de la marca obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<MarcaConPreciosDto>.ErrorResponse(
                    "Error al obtener los precios de la marca.",
                    ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> GuardarMasivoAsync ( List<PrecioTarifaDto> listaPreciosDto )
        {
            try
            {
                if (listaPreciosDto is null || !listaPreciosDto.Any())
                    return ApiResponse<bool>.ErrorResponse("Lista vacía.", "No se recibieron datos para guardar.", 400);

                var entidades = listaPreciosDto.Select(p => new PrecioTarifa
                {
                    MaterialId = p.MaterialId,
                    MarcaId = p.MarcaId,
                    ProveedorCifId = p.ProveedorCifId,
                    Precio = p.Precio
                }).ToList();

                var resultado = await _precioTarifaRepository.GuardarMasivoAsync(entidades);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Precios cargados correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo realizar la carga masiva.", "Error en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error en la carga masiva de precios.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<PrecioTarifaDto>> ObtenerPrecioMasBajoDeMaterialAsync ( int materialId )
        {
            try
            {
                var precio = await _precioTarifaRepository.ObtenerPrecioMasBajoAsync(materialId);

                if (precio is null)
                {
                    return ApiResponse<PrecioTarifaDto>.ErrorResponse(
                        "Precio no encontrado.",
                        $"No hay precios registrados para el material con ID {materialId}.", 404);
                }

                var resultado = new PrecioTarifaDto
                {
                    MaterialId = precio.MaterialId,
                    MarcaId = precio.MarcaId,
                    ProveedorCifId = precio.ProveedorCifId,
                    Precio = precio.Precio
                };

                return ApiResponse<PrecioTarifaDto>.SuccessResponse(resultado, "Precio mínimo obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<PrecioTarifaDto>.ErrorResponse("Error al obtener el precio mínimo.", ex.Message, 500);
            }
        }
    }
}
