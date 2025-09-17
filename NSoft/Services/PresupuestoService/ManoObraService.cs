using Microsoft.EntityFrameworkCore;
using NSoft.DTOs;
using NSoft.DTOs.NSoft.DTOs.Presupuesto;
using NSoft.Models.Presupuesto;
using NSoft.Repositories.PresupuestoRepository.IRepositories;
using NSoft.Services.PresupuestoService.IPresupuestoServices;

namespace NSoft.Services.PresupuestoService
{
    public class ManoObraService:IManoObraService
    {
        private readonly IManoObraRepository _repo;

        public ManoObraService ( IManoObraRepository repo )
        {
            _repo = repo;
        }

        // Helper centralizado para mapear excepciones a ApiResponse (incluye ex.Message en DEV)
        private static ApiResponse<T> FromException<T> ( string userMessage, Exception ex ) // ADDED
        {
            return ex switch
            {
                KeyNotFoundException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 404),
                DbUpdateConcurrencyException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 409),
                DbUpdateException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 409),
                ArgumentException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 400),
                _ => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 500)
            };
        }

        // ===== Obtener por Id (incluye TipoManoObra) =====
        public async Task<ApiResponse<ManoDeObraDto>> ObtenerPorIdAsync ( int manoObraId )
        {
            try
            {
                if (manoObraId <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");

                var entidad = await _repo.ObtenerPorIdConTipoAsync(manoObraId);
                if (entidad is null)
                    return ApiResponse<ManoDeObraDto>.ErrorResponse("No encontrado.", $"No existe ManoDeObra con ID {manoObraId}.", 404);

                return ApiResponse<ManoDeObraDto>.SuccessResponse(MapToDto(entidad), "Mano de obra obtenida correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<ManoDeObraDto>("Error al obtener la mano de obra.", ex);
            }
        }

        public async Task<ApiResponse<ManoDeObraDto>> CrearAsync ( ManoDeObraCreacionDto dto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(dto);
                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    throw new ArgumentException("El nombre es obligatorio.");
                if (dto.DescompuestoId <= 0)
                    throw new ArgumentException("El DescompuestoId es obligatorio y debe ser válido.");
                if (dto.TipoManoObraId <= 0)
                    throw new ArgumentException("El TipoManoObraId es obligatorio y debe ser válido.");
                if (dto.UnidadesRealizar < 0)
                    throw new ArgumentException("Las unidades a realizar no pueden ser negativas.");
                if (dto.Precio < 0)
                    throw new ArgumentException("El precio no puede ser negativo.");

                var entidad = new ManoDeObra
                {
                    Nombre = dto.Nombre,
                    UnidadesRealizar = dto.UnidadesRealizar,
                    Precio = dto.Precio,
                    DescompuestoId = dto.DescompuestoId,
                    TipoManoObraId = dto.TipoManoObraId
                };

                var creado = await _repo.CrearAsync(entidad);

                // Para devolver con TipoManoObra, podemos re-leer con Include (barato y claro)
                var conTipo = await _repo.ObtenerPorIdConTipoAsync(creado.ManoObraId) ?? creado;

                return ApiResponse<ManoDeObraDto>.SuccessResponse(MapToDto(conTipo), "Mano de obra creada correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<ManoDeObraDto>("Error al crear la mano de obra.", ex);
            }
        }

        // ===== Actualizar =====
        public async Task<ApiResponse<bool>> ActualizarAsync ( int manoObraId, ManoDeObraEdicionDto dto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(dto);
                if (manoObraId <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");
                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    throw new ArgumentException("El nombre es obligatorio.");
                if (dto.DescompuestoId <= 0)
                    throw new ArgumentException("El DescompuestoId es obligatorio y debe ser válido.");
                if (dto.TipoManoObraId <= 0)
                    throw new ArgumentException("El TipoManoObraId es obligatorio y debe ser válido.");
                if (dto.UnidadesRealizar < 0)
                    throw new ArgumentException("Las unidades a realizar no pueden ser negativas.");
                if (dto.Precio < 0)
                    throw new ArgumentException("El precio no puede ser negativo.");

                var entidad = new ManoDeObra
                {
                    ManoObraId = manoObraId,
                    Nombre = dto.Nombre,
                    UnidadesRealizar = dto.UnidadesRealizar,
                    Precio = dto.Precio,
                    DescompuestoId = dto.DescompuestoId,
                    TipoManoObraId = dto.TipoManoObraId
                };

                await _repo.ActualizarAsync(entidad);
                return ApiResponse<bool>.SuccessResponse(true, "Mano de obra actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al actualizar la mano de obra.", ex);
            }
        }

        // ===== Eliminar =====
        public async Task<ApiResponse<bool>> EliminarAsync ( int manoObraId )
        {
            try
            {
                if (manoObraId <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");

                await _repo.EliminarAsync(manoObraId);
                return ApiResponse<bool>.SuccessResponse(true, "Mano de obra eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al eliminar la mano de obra.", ex);
            }
        }

        // =======================
        // Mapeo privado
        // =======================
        private static ManoDeObraDto MapToDto ( ManoDeObra e ) // ADDED
        {
            return new ManoDeObraDto
            {
                ManoDeObraId = e.ManoObraId,
                Nombre = e.Nombre,
                UnidadesRealizar = e.UnidadesRealizar,
                Precio = e.Precio,
                TipoManoObraId = e.TipoManoObraId,
                TipoManoObra = e.TipoManoObra is null
                    ? null
                    : new TipoManoObraDto
                    {
                        TipoManoObraId = e.TipoManoObra.TipoManoObraId,
                        Nombre = e.TipoManoObra.Nombre
                    }
            };
        }
    }
}
