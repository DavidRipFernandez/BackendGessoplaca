using Microsoft.EntityFrameworkCore;
using NSoft.DTOs;
using NSoft.Models.Presupuesto;
using NSoft.Repositories.PresupuestoRepository.IRepositories;
using NSoft.Services.PresupuestoService.IPresupuestoServices;

namespace NSoft.Services.PresupuestoServices
{
    public class DescompuestoService : IDescompuestoService
    {
        private readonly IDescompuestoRepository _repo;

        public DescompuestoService ( IDescompuestoRepository repo )
        {
            _repo = repo;
        }

        // Helper centralizado para mapear excepciones a ApiResponse (incluye ex.Message en DEV)
        private static ApiResponse<T> FromException<T> ( string userMessage, Exception ex )
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

        // ========= Listados =========
        public async Task<ApiResponse<List<DescompuestoDto>>> ObtenerPlantillasAsync ()
        {
            try
            {
                var plantillas = await _repo.ObtenerPlantillas();
                var data = plantillas.Select(e => MapToDto(e, incluirDetalles: false)).ToList();
                return ApiResponse<List<DescompuestoDto>>.SuccessResponse(data, "Plantillas obtenidas correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<List<DescompuestoDto>>("Error al obtener plantillas.", ex);
            }
        }

        public async Task<ApiResponse<List<DescompuestoDto>>> BuscarPorNombreAsync ( string nombre )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    throw new ArgumentException("El nombre de búsqueda es obligatorio.");

                var encontrados = await _repo.BuscarPorNombreAsync(nombre);
                var data = encontrados.Select(e => MapToDto(e, incluirDetalles: false)).ToList();
                return ApiResponse<List<DescompuestoDto>>.SuccessResponse(data, "Búsqueda realizada correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<List<DescompuestoDto>>("Error al buscar descompuestos por nombre.", ex);
            }
        }

        // ========= Lectura por Id con detalles =========
        public async Task<ApiResponse<DescompuestoDto>> ObtenerConDetallesAsync ( int descompuestoId )
        {
            try
            {
                if (descompuestoId <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");

                var entidad = await _repo.ObtenerConDetallesAsync(descompuestoId);
                if (entidad is null)
                    return ApiResponse<DescompuestoDto>.ErrorResponse("No encontrado.", $"No existe descompuesto con ID {descompuestoId}.", 404);

                var dto = MapToDto(entidad, incluirDetalles: true);
                return ApiResponse<DescompuestoDto>.SuccessResponse(dto, "Descompuesto obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<DescompuestoDto>("Error al obtener el descompuesto.", ex);
            }
        }

        // ========= Crear =========
        public async Task<ApiResponse<DescompuestoDto>> CrearAsync ( DescompuestoCreacionDto descompuestoDto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(descompuestoDto);

                if (string.IsNullOrWhiteSpace(descompuestoDto.Titulo))
                    throw new ArgumentException("El título es obligatorio.");
                if (string.IsNullOrWhiteSpace(descompuestoDto.UnidadMedida))
                    throw new ArgumentException("La unidad de medida es obligatoria.");

                var entidad = new Descompuesto
                {
                    // CHANGED: mapeo completo según tu entidad/DTO
                    Titulo = descompuestoDto.Titulo,
                    Descripcion = descompuestoDto.Descripcion,
                    UnidadMedida = descompuestoDto.UnidadMedida,
                    Beneficio = descompuestoDto.Beneficio,
                    ManoObra = 0m,                 // ADDED: no viene en Creación → por defecto
                    GastoAdministrativo = 0m,      // ADDED: no viene en Creación → por defecto
                    Precio = 0m,                   // ADDED: si tu entidad lo tiene, iniciamos en 0
                    Estado = true,                 // ADDED: alta por defecto
                    IsPlantilla = descompuestoDto.IsPlantilla,
                    PresupuestoId = descompuestoDto.PresupuestoId
                };

                var creado = await _repo.AgregarAsync(entidad);
                var dto = MapToDto(creado, incluirDetalles: false);
                return ApiResponse<DescompuestoDto>.SuccessResponse(dto, "Descompuesto creado correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<DescompuestoDto>("Error al crear el descompuesto.", ex);
            }
        }

        // ========= Actualizar parcial =========
        public async Task<ApiResponse<bool>> ActualizarParcialAsync ( int descompuestoId, DescompuestoEdicionParcialDto descompuestoDto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(descompuestoDto);
                if (descompuestoId <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");
                if (string.IsNullOrWhiteSpace(descompuestoDto.Titulo))
                    throw new ArgumentException("El título es obligatorio.");
                if (string.IsNullOrWhiteSpace(descompuestoDto.UnidadMedida))
                    throw new ArgumentException("La unidad de medida es obligatoria.");

                await _repo.ActualizarParcialAsync(
                    descompuestoId,
                    descompuestoDto.Titulo,
                    descompuestoDto.Descripcion ?? string.Empty,
                    descompuestoDto.UnidadMedida,
                    descompuestoDto.Beneficio,
                    descompuestoDto.ManoObra,
                    descompuestoDto.GastoAdministrativo);

                return ApiResponse<bool>.SuccessResponse(true, "Descompuesto actualizado parcialmente.");
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al actualizar parcialmente el descompuesto.", ex);
            }
        }

        // ========= Actualizar completo =========
        public async Task<ApiResponse<bool>> ActualizarCompletoAsync ( int descompuestoId, DescompuestoDto descompuestoDto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(descompuestoDto);
                if (descompuestoId <= 0 || descompuestoDto.DescompuestoId != descompuestoId)
                    throw new ArgumentException("El ID de ruta y el ID del objeto no coinciden o no son válidos.");
                if (string.IsNullOrWhiteSpace(descompuestoDto.Titulo))
                    throw new ArgumentException("El título es obligatorio.");
                if (string.IsNullOrWhiteSpace(descompuestoDto.UnidadMedida))
                    throw new ArgumentException("La unidad de medida es obligatoria.");

                var entidad = new Descompuesto
                {
                    // CHANGED: mapeo 1:1 con tu DTO completo
                    DescompuestoId = descompuestoDto.DescompuestoId,
                    Titulo = descompuestoDto.Titulo,
                    Descripcion = descompuestoDto.Descripcion,
                    IsPlantilla = descompuestoDto.IsPlantilla,
                    UnidadMedida = descompuestoDto.UnidadMedida,
                    Precio = descompuestoDto.Precio,
                    ManoObra = descompuestoDto.ManoObra,
                    Beneficio = descompuestoDto.Beneficio,
                    GastoAdministrativo = descompuestoDto.GastoAdministrativo,
                    Estado = descompuestoDto.Estado,
                    PresupuestoId = descompuestoDto.PresupuestoId
                    // NOTA: colecciones (DetalleDescompuestos/ManoDeObras) se actualizan en endpoints/servicios específicos.
                };

                await _repo.ActualizarCompletoAsync(entidad);
                return ApiResponse<bool>.SuccessResponse(true, "Descompuesto actualizado completamente.");
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al actualizar el descompuesto.", ex);
            }
        }

        // ========= Eliminar =========
        public async Task<ApiResponse<bool>> EliminarAsync ( int descompuestoId )
        {
            try
            {
                if (descompuestoId <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero.");

                await _repo.EliminarAsync(descompuestoId);
                return ApiResponse<bool>.SuccessResponse(true, "Descompuesto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return FromException<bool>("Error al eliminar el descompuesto.", ex);
            }
        }

        // =======================
        // Mapeos (privados)
        // =======================
        private static DescompuestoDto MapToDto ( Descompuesto e, bool incluirDetalles )
        {
            var dto = new DescompuestoDto
            {
                DescompuestoId = e.DescompuestoId,
                Titulo = e.Titulo,
                Descripcion = e.Descripcion,
                IsPlantilla = e.IsPlantilla,
                UnidadMedida = e.UnidadMedida,
                Precio = e.Precio,                           // ADDED
                ManoObra = e.ManoObra,
                Beneficio = e.Beneficio,
                GastoAdministrativo = e.GastoAdministrativo,
                Estado = e.Estado,                           // ADDED
                PresupuestoId = e.PresupuestoId              // ADDED
            };

            if (incluirDetalles)
            {
                dto.DetalleDescompuestos = e.DetalleDescompuestos?.Select(d => new DetalleDescompuestoDto
                {
                    DetalleDescompuestoId = d.DetalleDescompuestoId,
                    NombreMaterial = d.NombreMaterial,
                    Proveedor = d.Proveedor,
                    Unidades = d.Unidades,
                    Precio = d.Precio,
                    Descuento = d.Descuento,
                    Estado = d.Estado
                }).ToList() ?? new List<DetalleDescompuestoDto>();

                dto.ManoDeObras = e.ManoDeObras?.Select(m => new ManoDeObraDto
                {
                    ManoDeObraId = m.ManoObraId,
                    Nombre = m.Nombre,
                    UnidadesRealizar = m.UnidadesRealizar,
                    Precio = m.Precio,
                    TipoManoObraId = m.TipoManoObraId
                }).ToList() ?? new List<ManoDeObraDto>();
            }

            return dto;
        }
    }
}
