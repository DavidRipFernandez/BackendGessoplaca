using Microsoft.EntityFrameworkCore;
using NSoft.DTOs;
using NSoft.Models.Presupuesto;
using NSoft.Repositories.PresupuestoRepository.IRepositories;
using NSoft.Services.PresupuestoService.IPresupuestoServices;

namespace NSoft.Services.PresupuestoService
{
    public class DetalleDescompuestoService:IDetalleDescompuestoService
    {
        private readonly IDetalleDescompuestoRepository _repo;

        public DetalleDescompuestoService ( IDetalleDescompuestoRepository repo )
        {
            _repo = repo;
        }

        // Helper para centralizar la creación de respuestas de error.
        private static ApiResponse<T> FromException<T> ( string userMessage, Exception ex )
        {
            return ex switch
            {
                KeyNotFoundException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 404),
                InvalidOperationException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 409),
                DbUpdateConcurrencyException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 409),
                DbUpdateException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 409),
                ArgumentException => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 400),
                _ => ApiResponse<T>.ErrorResponse(userMessage, ex.Message, 500)
            };
        }

        // ========= Crear (SIN CAMBIOS) =========
        public async Task<ApiResponse<DetalleDescompuestoDto>> CrearAsync ( DetalleDescompuestoCreacionDto detalleDto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(detalleDto);
                var entidad = new DetalleDescompuesto
                {
                    NombreMaterial = detalleDto.NombreMaterial,
                    Proveedor = detalleDto.Proveedor,
                    Marca = detalleDto.Marca,
                    Unidades = detalleDto.Unidades,
                    Precio = detalleDto.Precio,
                    Descuento = detalleDto.Descuento,
                    DescompuestoId = detalleDto.DescompuestoId,
                };
                await _repo.AgregarAsync(entidad);
                var dtoCreado = MapToDto(entidad);
                return ApiResponse<DetalleDescompuestoDto>.SuccessResponse(dtoCreado, "Detalle creado correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<DetalleDescompuestoDto>("Error al crear el detalle.", ex);
            }
        }

        // ========= Actualizar =========
        public async Task<ApiResponse<bool>> ActualizarAsync ( int detalleId, DetalleDescompuestoEdicionDto detalleDto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(detalleDto);
                if (detalleId <= 0 || detalleDto.DetalleDescompuestoId != detalleId)
                    throw new ArgumentException("El ID de la ruta y el del objeto no coinciden o no son válidos.");

                var entidadParaVerificar = await _repo.ObtenerPorIdAsync(detalleId);

                if (entidadParaVerificar.Estado)
                    throw new InvalidOperationException("No se puede modificar un detalle que está activo.");

                var entidad = new DetalleDescompuesto
                {
                    DetalleDescompuestoId = detalleDto.DetalleDescompuestoId,
                    NombreMaterial = detalleDto.NombreMaterial,
                    Proveedor = detalleDto.Proveedor,
                    Marca = detalleDto.Marca,
                    Unidades = detalleDto.Unidades,
                    Precio = detalleDto.Precio,
                    Descuento = detalleDto.Descuento,
                    Estado = detalleDto.Estado,
                    DescompuestoId = detalleDto.DescompuestoId
                };

                await _repo.ActualizarAsync(entidad);
                return ApiResponse<bool>.SuccessResponse(true, "Detalle actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al actualizar el detalle.", ex);
            }
        }

        // ========= Eliminar =========
        public async Task<ApiResponse<bool>> EliminarAsync ( int detalleId )
        {
            try
            {
                if (detalleId <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");

                var entidadParaVerificar = await _repo.ObtenerPorIdAsync(detalleId);

                if (entidadParaVerificar.Estado)
                    throw new InvalidOperationException("No se puede eliminar un detalle que está activo.");

                await _repo.EliminarAsync(detalleId);
                return ApiResponse<bool>.SuccessResponse(true, "Detalle eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al eliminar el detalle.", ex);
            }
        }

        // =======================
        // Mapeo (privado) - (SIN CAMBIOS)
        // =======================
        private static DetalleDescompuestoDto MapToDto ( DetalleDescompuesto e )
        {
            return new DetalleDescompuestoDto
            {
                DetalleDescompuestoId = e.DetalleDescompuestoId,
                NombreMaterial = e.NombreMaterial,
                Proveedor = e.Proveedor,
                Marca = e.Marca,
                Unidades = e.Unidades,
                Precio = e.Precio,
                Descuento = e.Descuento,
                Estado = e.Estado,
                DescompuestoId = e.DescompuestoId
            };
        }
    }
}
