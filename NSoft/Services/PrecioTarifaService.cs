using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class PrecioTarifaService : IPrecioTarifaService
    {
        private readonly IPrecioTarifaRepository _precioTarifaRepository;
        //Dependecias para resolver por nombre y validar relacion proveedor-marca
        private readonly IMaterialRepository _materialRepository;
        private readonly IMarcaRepository _marcaRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProveedorMarcaRepository _proveedorMarcaRepository;

        public PrecioTarifaService(
            IPrecioTarifaRepository precioTarifaRepository,
            IMaterialRepository materialRepository,
            IMarcaRepository marcaRepository,
            ISupplierRepository supplierRepository,
            IProveedorMarcaRepository proveedorMarcaRepository
        )
        {
            _precioTarifaRepository = precioTarifaRepository;
            _materialRepository = materialRepository;          
            _marcaRepository = marcaRepository;                
            _supplierRepository = supplierRepository;        
            _proveedorMarcaRepository = proveedorMarcaRepository; 
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

        public async Task<ApiResponse<object>> CargarPreciosPorNombresAsync(
            string proveedorCifId,
            string? empresa,
            List<CargaPrecioItemRequestDto> filas)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(proveedorCifId))
                    return ApiResponse<object>.ErrorResponse("Proveedor no válido.", "El proveedorCifId es requerido.", 400);

                if (filas is null || filas.Count == 0)
                    return ApiResponse<object>.ErrorResponse("Lista vacía.", "No se enviaron filas para procesar.", 400);

                // (a) validar que el proveedor exista
                var proveedor = await _supplierRepository.ObtenerPorIdAsync(proveedorCifId);
                if (proveedor is null)
                    return ApiResponse<object>.ErrorResponse(
                        "Proveedor inexistente.",
                        $"No se encontró proveedor con CIF '{proveedorCifId}'.",
                        404);

                var errores = new List<CargaPrecioItemErrorDto>();

                foreach (var fila in filas)
                {
                    var nombreMat = fila.NombreMaterial?.Trim();
                    var nombreMarca = fila.NombreMarca?.Trim();

                    // Campos requeridos
                    if (string.IsNullOrWhiteSpace(nombreMat) || string.IsNullOrWhiteSpace(nombreMarca))
                    {
                        errores.Add(new CargaPrecioItemErrorDto
                        {
                            NombreMaterial = nombreMat ?? "",
                            NombreMarca = nombreMarca ?? "",
                            Fila = fila.FilaExcel,
                            DetalleError = "Nombre de material y nombre de marca son obligatorios."
                        });
                        continue;
                    }

                    if (fila.Precio <= 0)
                    {
                        errores.Add(new CargaPrecioItemErrorDto
                        {
                            NombreMaterial = nombreMat,
                            NombreMarca = nombreMarca,
                            Fila = fila.FilaExcel,
                            DetalleError = "El precio debe ser mayor a 0."
                        });
                        continue;
                    }

                    // (a) buscar material y marca por nombre
                    var material = await _materialRepository.BuscarPorNombreAsync(nombreMat);
                    var marca = await _marcaRepository.BuscarPorNombreAsync(nombreMarca);

                    if (material is null || marca is null)
                    {
                        errores.Add(new CargaPrecioItemErrorDto
                        {
                            MaterialId = material?.MaterialId,
                            NombreMaterial = nombreMat,
                            MarcaId = marca?.MarcaId,
                            NombreMarca = nombreMarca,
                            Fila = fila.FilaExcel,
                            DetalleError = "Verifique que el material y la marca existan correctamente."
                        });
                        continue;
                    }

                    // (d) validar relación Proveedor–Marca
                    var relacion = await _proveedorMarcaRepository.ObtenerPorIdAsync(proveedorCifId, marca.MarcaId);
                    if (relacion is null || !relacion.Estado)
                    {
                        errores.Add(new CargaPrecioItemErrorDto
                        {
                            MaterialId = material.MaterialId,
                            NombreMaterial = nombreMat,
                            MarcaId = marca.MarcaId,
                            NombreMarca = nombreMarca,
                            Fila = fila.FilaExcel,
                            DetalleError = "La marca no está asociada al proveedor o está desactivada."
                        });
                        continue;
                    }

                    // (b) upsert de PreciosTarifa (tu repo ya realiza upsert)
                    var entidad = new PrecioTarifa
                    {
                        MaterialId = material.MaterialId,
                        MarcaId = marca.MarcaId,
                        ProveedorCifId = proveedorCifId,
                        Precio = fila.Precio
                    };

                    var ok = await _precioTarifaRepository.GuardarPrecioAsync(entidad);
                    if (!ok)
                    {
                        errores.Add(new CargaPrecioItemErrorDto
                        {
                            MaterialId = material.MaterialId,
                            NombreMaterial = nombreMat,
                            MarcaId = marca.MarcaId,
                            NombreMarca = nombreMarca,
                            Fila = fila.FilaExcel,
                            DetalleError = "No se pudo guardar/actualizar el precio."
                        });
                    }
                }

                // Respuesta final
                if (errores.Count == 0)
                {
                    return new ApiResponse<object>(
                        message: "Todos los materiales han sido registrados correctamente",
                        technicalMessage: "",
                        data: Array.Empty<object>(),
                        statusCode: 200,
                        success: true
                    );
                }
                else
                {
                    var payload = new List<CargaPreciosResultadoErrorDto>
                    {
                        new CargaPreciosResultadoErrorDto
                        {
                            ProveedorCifId = proveedorCifId,
                            Empresa = empresa,
                            Materiales = errores
                        }
                    };

                    return new ApiResponse<object>(
                        message: $"Se han encontrado algunos problemas al registrar los materiales del proveedor : {proveedorCifId}",
                        technicalMessage: "",
                        data: payload,
                        statusCode: 200,   // si prefieres marcarlo como fallo, puedes usar success=false o 207
                        success: true
                    );
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.ErrorResponse("Error interno al procesar la carga masiva.", ex.Message, 500);
            }
        }
    }
}
