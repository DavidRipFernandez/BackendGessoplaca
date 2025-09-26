using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.IServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecioTarifaController : ControllerBase
    {
        private readonly IPrecioTarifaService _precioTarifaService;

        public PrecioTarifaController ( IPrecioTarifaService precioTarifaService )
        {
            _precioTarifaService = precioTarifaService;
        }

        [HttpPost]
        public async Task<IActionResult> Guardar ( [FromBody] PrecioTarifaDto dto )
        {
            var response = await _precioTarifaService.GuardarPrecioAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("carga-masiva")]
        public async Task<IActionResult> GuardarMasivo ( [FromBody] List<PrecioTarifaDto> listaPrecios )
        {
            var response = await _precioTarifaService.GuardarMasivoAsync(listaPrecios);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("proveedor/{proveedorCifId}")]
        public async Task<IActionResult> ObtenerPorProveedor ( string proveedorCifId )
        {
            var response = await _precioTarifaService.ObtenerPreciosPorProveedorAsync(proveedorCifId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("marca/{marcaId:int}")]
        public async Task<IActionResult> ObtenerPorMarca ( int marcaId )
        {
            var response = await _precioTarifaService.ObtenerPrecioPorMarcaAsync(marcaId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("material/{materialId:int}/minimo")]
        public async Task<IActionResult> ObtenerPrecioMasBajo ( int materialId )
        {
            var response = await _precioTarifaService.ObtenerPrecioMasBajoDeMaterialAsync(materialId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("proveedor/{proveedorCifId}/carga-masiva-nombres")]
        public async Task<IActionResult> CargarPreciosPorNombres(string proveedorCifId,[FromBody] CargaPreciosRequestDto request)
        {
            // Validación rápida de entrada
            if (request is null || request.MaterialesProveedor is null || request.MaterialesProveedor.Count == 0)
            {
                var error = ApiResponse<object>.ErrorResponse(
                    "Lista vacía.",
                    "No se enviaron filas para procesar.",
                    400
                );
                return StatusCode(error.StatusCode, error);
            }

            var response = await _precioTarifaService.CargarPreciosPorNombresAsync(
                proveedorCifId,
                request.Empresa,
                request.MaterialesProveedor
            );

            return StatusCode(response.StatusCode, response);
        }




    }
}
