using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class MarcaService:IMarcaService
    {

        private readonly IMarcaRepository _marcaRepository;

        public MarcaService (IMarcaRepository marcaRepository )
        {
            _marcaRepository = marcaRepository;
        }

        private MarcaDto MapearADto ( Marca marca )
        {
            return new MarcaDto
            {
                MarcaId = marca.MarcaId,
                Nombre = marca.Nombre,
                Descripcion = marca.Descripcion,
                Estado = marca.Estado
            };
        }

        private List<MarcaDto> MapearLista ( IEnumerable<Marca> marcas )
        {
            return marcas.Select(MapearADto).ToList();
        }

        public async Task<ApiResponse<List<MarcaDto>>> ObtenerActivosAsync ()
        {
            try
            {
                var marcas = await _marcaRepository.ObtenerPorEstadoAsync(true);
                var resultado = MapearLista(marcas);

                return ApiResponse<List<MarcaDto>>.SuccessResponse(resultado, "Marcas activas obtenidas correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<MarcaDto>>.ErrorResponse("Error al obtener marcas activas.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<List<MarcaDto>>> ObtenerEliminadosAsync ()
        {
            try
            {
                var marcas = await _marcaRepository.ObtenerPorEstadoAsync(false);
                var resultado = MapearLista(marcas);

                return ApiResponse<List<MarcaDto>>.SuccessResponse(resultado, "Marcas eliminadas obtenidas correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<MarcaDto>>.ErrorResponse("Error al obtener marcas eliminadas.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<MarcaDto>> ObtenerPorIdAsync ( int id )
        {
            try
            {
                var marca = await _marcaRepository.ObtenerPorIdAsync(id);
                var dto = MapearADto(marca);

                return ApiResponse<MarcaDto>.SuccessResponse(dto, "Marca obtenida correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return ApiResponse<MarcaDto>.ErrorResponse("Marca no encontrada.", ex.Message, 404);
            }
            catch (Exception ex)
            {
                return ApiResponse<MarcaDto>.ErrorResponse("Error al obtener la marca.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> AgregarAsync ( MarcaDto marca )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(marca);

                var nueva = new Marca
                {
                    Nombre = marca.Nombre,
                    Descripcion = marca.Descripcion,
                    Estado = true
                };

                var resultado = await _marcaRepository.AgregarAsync(nueva);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Marca agregada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo agregar la marca.", "Fallo en el repositorio.", 500);
            }
            catch (ArgumentNullException ex)
            {
                return ApiResponse<bool>.ErrorResponse("Faltan datos para agregar la marca.", ex.Message, 400);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al agregar la marca.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync ( MarcaDto marca )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(marca);

                if (marca.MarcaId == 0)
                    return ApiResponse<bool>.ErrorResponse("ID requerido.", "El ID de la marca es necesario para actualizar.", 400);

                var marcaActualizada = new Marca
                {
                    MarcaId = marca.MarcaId,
                    Nombre = marca.Nombre,
                    Descripcion = marca.Descripcion
                };

                var resultado = await _marcaRepository.ActualizarAsync(marcaActualizada);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Marca actualizada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo actualizar la marca.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al actualizar la marca.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync ( int id )
        {
            try
            {
                var resultado = await _marcaRepository.CambiarEstadoAsync(id, false);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Marca eliminada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo eliminar la marca.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al eliminar la marca.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ReactivarAsync ( int id )
        {
            try
            {
                var resultado = await _marcaRepository.CambiarEstadoAsync(id, true);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Marca reactivada correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo reactivar la marca.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al reactivar la marca.", ex.Message, 500);
            }
        }
    }
}
