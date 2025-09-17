using Microsoft.EntityFrameworkCore;
using NSoft.DTOs;
using NSoft.Models.Presupuesto;
using NSoft.Repositories.PresupuestoRepository.IRepositories;
using NSoft.Services.PresupuestoService.IPresupuestoService;

namespace NSoft.Services.PresupuestoServices
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IPresupuestoRepository _presupuestoRepository;
        private readonly ILogger<PresupuestoService> _logger;

        public PresupuestoService ( IPresupuestoRepository repositorio, ILogger<PresupuestoService> logger )
        {
            _presupuestoRepository = repositorio;
            _logger = logger;
        }

        public async Task<ApiResponse<List<PresupuestoDto>>> ListarPendientesAsync ()
        {
            try
            {
                var presupuestos = await _presupuestoRepository.ListarPorEstadoAsync("Pendiente");
                var listaDto = presupuestos.Select(p => MapearEntidadADto(p)).ToList();
                return ApiResponse<List<PresupuestoDto>>.SuccessResponse(listaDto, "Presupuestos pendientes obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener presupuestos pendientes.");
                return ApiResponse<List<PresupuestoDto>>.ErrorResponse("Ocurrió un error al obtener los presupuestos.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<List<PresupuestoDto>>> ListarEnCursoAsync ()
        {
            try
            {
                var presupuestos = await _presupuestoRepository.ListarPorEstadoAsync("En-Curso");
                var listaDto = presupuestos.Select(p => MapearEntidadADto(p)).ToList();
                return ApiResponse<List<PresupuestoDto>>.SuccessResponse(listaDto, "Presupuestos en curso obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener presupuestos en curso.");
                return ApiResponse<List<PresupuestoDto>>.ErrorResponse("Ocurrió un error al obtener los presupuestos.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<List<PresupuestoDto>>> ListarRechazadosAsync ()
        {
            try
            {
                var presupuestos = await _presupuestoRepository.ListarPorEstadoAsync("Rechazado");
                var listaDto = presupuestos.Select(p => MapearEntidadADto(p)).ToList();
                return ApiResponse<List<PresupuestoDto>>.SuccessResponse(listaDto, "Presupuestos rechazados obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener presupuestos rechazados.");
                return ApiResponse<List<PresupuestoDto>>.ErrorResponse("Ocurrió un error al obtener los presupuestos.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<PresupuestoDto>> ObtenerPorIdAsync ( int presupuestoId )
        {
            try
            {
                var presupuesto = await _presupuestoRepository.ObtenerPorIdConDetallesAsync(presupuestoId);
                if (presupuesto == null)
                {
                    return ApiResponse<PresupuestoDto>.ErrorResponse("Presupuesto no encontrado.", $"No se encontró el presupuesto con ID {presupuestoId}.", 404);
                }
                var dto = MapearEntidadADto(presupuesto);
                return ApiResponse<PresupuestoDto>.SuccessResponse(dto, "Presupuesto obtenido con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el presupuesto con ID {Id}", presupuestoId);
                return ApiResponse<PresupuestoDto>.ErrorResponse("Ocurrió un error al obtener el presupuesto.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<PresupuestoDto>> CrearAsync ( PresupuestoDto presupuestoDto )
        {
            try
            {
                var entidad = MapearDtoAEntidad(presupuestoDto);
                var entidadCreada = await _presupuestoRepository.CrearAsync(entidad);
                var dtoCreado = MapearEntidadADto(entidadCreada);
                return ApiResponse<PresupuestoDto>.SuccessResponse(dtoCreado, "Presupuesto creado correctamente.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error de base de datos al crear el presupuesto.");
                return ApiResponse<PresupuestoDto>.ErrorResponse("Conflicto al crear el recurso.", "Posible dato duplicado.", 409);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el presupuesto.");
                return ApiResponse<PresupuestoDto>.ErrorResponse("Ocurrió un error inesperado.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync ( int presupuestoId, PresupuestoDto presupuestoDto )
        {
            try
            {
                if (presupuestoId != presupuestoDto.PresupuestoId)
                {
                    return ApiResponse<bool>.ErrorResponse("Conflicto de IDs.", "El ID de la ruta no coincide con el del objeto.", 400);
                }
                var entidad = MapearDtoAEntidad(presupuestoDto);
                await _presupuestoRepository.ActualizarAsync(entidad);
                return ApiResponse<bool>.SuccessResponse(true, "Presupuesto actualizado correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return ApiResponse<bool>.ErrorResponse("Recurso no encontrado.", ex.Message, 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el presupuesto con ID {Id}", presupuestoId);
                return ApiResponse<bool>.ErrorResponse("Ocurrió un error al actualizar.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync ( int presupuestoId )
        {
            try
            {
                await _presupuestoRepository.EliminarAsync(presupuestoId);
                return ApiResponse<bool>.SuccessResponse(true, "Presupuesto eliminado correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return ApiResponse<bool>.ErrorResponse("Recurso no encontrado.", ex.Message, 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el presupuesto con ID {Id}", presupuestoId);
                return ApiResponse<bool>.ErrorResponse("Ocurrió un error al eliminar.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<PresupuestoDto>> ClonarAsync ( int presupuestoOriginalId, string nuevoNombre )
        {
            try
            {
                var entidadClonada = await _presupuestoRepository.ClonarAsync(presupuestoOriginalId, nuevoNombre);
                var dtoClonado = MapearEntidadADto(entidadClonada);
                return ApiResponse<PresupuestoDto>.SuccessResponse(dtoClonado, "Presupuesto clonado con éxito.");
            }
            catch (KeyNotFoundException ex)
            {
                return ApiResponse<PresupuestoDto>.ErrorResponse("Recurso no encontrado.", ex.Message, 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al clonar el presupuesto con ID {Id}", presupuestoOriginalId);
                return ApiResponse<PresupuestoDto>.ErrorResponse("Ocurrió un error al clonar.", ex.Message, 500);
            }
        }

        // --- MÉTODOS DE MAPEO MANUAL ---

        private Presupuesto MapearDtoAEntidad ( PresupuestoDto dto )
        {
            return new Presupuesto
            {
                PresupuestoId = dto.PresupuestoId,
                Referencia = dto.Referencia,
                NombreEmpresa = dto.NombreEmpresa,
                CIF = dto.CIF,
                NombreContacto = dto.NombreContacto,
                Direccion = dto.Direccion,
                Poblacion = dto.Poblacion,
                Provincia = dto.Provincia,
                CodigoPostal = dto.CodigoPostal,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Estado = dto.Estado,
                FechaLimitePresentacion = dto.FechaLimitePresentacion,
                TotalPresupuesto = dto.TotalPresupuesto
            };
        }

        private PresupuestoDto MapearEntidadADto ( Presupuesto entidad )
        {
            return new PresupuestoDto
            {
                PresupuestoId = entidad.PresupuestoId,
                Referencia = entidad.Referencia,
                NombreEmpresa = entidad.NombreEmpresa,
                CIF = entidad.CIF,
                NombreContacto = entidad.NombreContacto,
                Direccion = entidad.Direccion,
                Poblacion = entidad.Poblacion,
                Provincia = entidad.Provincia,
                CodigoPostal = entidad.CodigoPostal,
                Telefono = entidad.Telefono,
                Email = entidad.Email,
                Estado = entidad.Estado,
                FechaLimitePresentacion = entidad.FechaLimitePresentacion,
                TotalPresupuesto = entidad.TotalPresupuesto,
                Descompuestos = entidad.Descompuestos?.Select(d => new DescompuestoDto
                {
                    DescompuestoId = d.DescompuestoId,
                    Titulo = d.Titulo,
                    Descripcion = d.Descripcion,
                    UnidadMedida = d.UnidadMedida,
                    Precio = d.Precio,
                    ManoObra = d.ManoObra,
                    Beneficio = d.Beneficio,
                    GastoAdministrativo = d.GastoAdministrativo,
                    IsPlantilla = d.IsPlantilla,
                    Estado = d.Estado,
                    PresupuestoId = d.PresupuestoId,
                }).ToList() ?? new List<DescompuestoDto>()
            };
        }

    }
}
