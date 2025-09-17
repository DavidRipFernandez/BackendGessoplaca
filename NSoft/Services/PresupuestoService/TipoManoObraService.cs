using Microsoft.EntityFrameworkCore;
using NSoft.DTOs;
using NSoft.DTOs.NSoft.DTOs.Presupuesto;
using NSoft.Models.Presupuesto;
using NSoft.Repositories.PresupuestoRepository.IRepositories;
using NSoft.Services.PresupuestoService.IPresupuestoServices;

namespace NSoft.Services.PresupuestoService
{
    public class TipoManoObraService:ITipoManoObraService
    {
        private readonly ITipoManoObraRepository _tipoManoObraRepository;

        public TipoManoObraService ( ITipoManoObraRepository repo )
        {
            _tipoManoObraRepository = repo;
        }

        // ADDED: Helper centralizado para mapear excepciones a ApiResponse (DEV incluye ex.Message)
        private static ApiResponse<T> FromException<T> ( string userMessage, Exception ex ) // ADDED
        {
            return ex switch
            {
                KeyNotFoundException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 404),
                DbUpdateConcurrencyException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 409),
                DbUpdateException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 409),
                ArgumentException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 400),
                _ => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 500),
            };
        }

        // CHANGED: factor común para listados por estado (DRY) manteniendo endpoints separados
        private async Task<List<TipoManoObraDto>> ListarPorEstadoInternalAsync ( bool estado ) // ADDED
        {
            var entities = await _tipoManoObraRepository.ListarPorEstadoAsync(estado);
            return entities.Select(e => new TipoManoObraDto
            {
                TipoManoObraId = e.TipoManoObraId,
                Nombre = e.Nombre,
                Estado = e.Estado
            }).ToList();
        }

        public async Task<ApiResponse<List<TipoManoObraDto>>> ListarActivosAsync ()
        {
            try
            {
                var dtos = await ListarPorEstadoInternalAsync(true); // ADDED
                return ApiResponse<List<TipoManoObraDto>>.SuccessResponse(dtos, "Tipos de Mano de Obra activos obtenidos correctamente");
            }
            catch (Exception ex)
            {
                return FromException<List<TipoManoObraDto>>("No se pudieron listar los tipos de mano de obra activos.", ex); // ADDED
            }
        }

        public async Task<ApiResponse<List<TipoManoObraDto>>> ListarInactivosAsync ()
        {
            try
            {
                var dtos = await ListarPorEstadoInternalAsync(false); // ADDED
                return ApiResponse<List<TipoManoObraDto>>.SuccessResponse(dtos, "Tipos de Mano de Obra inactivos obtenidos correctamente");
            }
            catch (Exception ex)
            {
                return FromException<List<TipoManoObraDto>>("No se pudieron listar los tipos de mano de obra inactivos.", ex); // ADDED
            }
        }

        public async Task<ApiResponse<TipoManoObraDetalleDto>> ObtenerPorIdAsync ( int id )
        {
            try
            {
                var entity = await _tipoManoObraRepository.ObtenerPorIdAsync(id);
                if (entity is null)
                    throw new KeyNotFoundException($"No se encontró TipoManoObra con Id = {id}."); // ADDED

                var dto = new TipoManoObraDetalleDto
                {
                    TipoManoObraId = entity.TipoManoObraId,
                    Nombre = entity.Nombre,
                    Estado = entity.Estado,
                    ManoDeObras = entity.ManoDeObras?.Select(m => new ManoDeObraDto
                    {
                        ManoDeObraId = m.ManoObraId, 
                        Nombre = m.Nombre,
                        UnidadesRealizar = m.UnidadesRealizar,
                        Precio = m.Precio
                    }).ToList() ?? new List<ManoDeObraDto>()
                };
                
                return ApiResponse<TipoManoObraDetalleDto>.SuccessResponse(dto, "Se obtuvo correctamente el tipo de mano de obra");
            }
            catch (Exception ex)
            {
                return FromException<TipoManoObraDetalleDto>("No se pudo obtener el tipo de mano de obra.", ex);
            }
        }

        public async Task<ApiResponse<TipoManoObraDto>> AgregarAsync ( TipoManoObraCreateDto dto )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    throw new ArgumentException("El nombre es obligatorio.", nameof(dto.Nombre));

                var entity = new TipoManoObra
                {
                    Nombre = dto.Nombre.Trim(),
                    Estado = dto.Estado
                };

                await _tipoManoObraRepository.AgregarAsync(entity); // ADDED

                var result = new TipoManoObraDto
                {
                    TipoManoObraId = entity.TipoManoObraId,
                    Nombre = entity.Nombre,
                    Estado = entity.Estado
                };

                return ApiResponse<TipoManoObraDto>.SuccessResponse(result, "Se agrego correctamente el tipo de Mano de Obra");
            }
            catch (Exception ex)
            {
                return FromException<TipoManoObraDto>("No se pudo crear el tipo de mano de obra.", ex);
            }
        }

        public async Task<ApiResponse<TipoManoObraDto>> ActualizarAsync ( int id, TipoManoObraUpdateDto dto )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    throw new ArgumentException("El nombre es obligatorio.", nameof(dto.Nombre));

                var entity = new TipoManoObra
                {
                    TipoManoObraId = id,
                    Nombre = dto.Nombre.Trim(),
                    Estado = dto.Estado
                };

                await _tipoManoObraRepository.ActualizarAsync(entity);

                var result = new TipoManoObraDto
                {
                    TipoManoObraId = entity.TipoManoObraId,
                    Nombre = entity.Nombre,
                    Estado = entity.Estado
                };

                return ApiResponse<TipoManoObraDto>.SuccessResponse(result, "Tipos de Mano de Obra se actualizo correctamente");
            }
            catch (Exception ex)
            {
                return FromException<TipoManoObraDto>("No se pudo actualizar el tipo de mano de obra.", ex);
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync ( int id )
        {
            try
            {
                await _tipoManoObraRepository.CambiarEstadoAsync(id, false);
                return ApiResponse<bool>.SuccessResponse(true, "Se elimino correctamente");
            }
            catch (Exception ex)
            {
                return FromException<bool>("No se pudo eliminar (desactivar) el tipo de mano de obra.", ex); // ADDED
            }
        }

        public async Task<ApiResponse<bool>> ReactivarAsync ( int id )
        {
            try
            {
                await _tipoManoObraRepository.CambiarEstadoAsync(id, true); // ADDED: soft delete
                return ApiResponse<bool>.SuccessResponse(true, "Se reactivo correctamente");
            }
            catch (Exception ex)
            {
                return FromException<bool>("No se pudo reactivar el tipo de mano de obra.", ex); // ADDED
            }
        }
    }
}
