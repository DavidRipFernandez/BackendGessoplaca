using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.PresupuestoService.IPresupuestoServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleDescompuestoController : ControllerBase
    {
        private readonly IDetalleDescompuestoService _service;

        public DetalleDescompuestoController ( IDetalleDescompuestoService service )
        {
            _service = service;
        }

        // POST: api/detalledescompuesto
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ApiResponse<DetalleDescompuestoDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<DetalleDescompuestoDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<DetalleDescompuestoDto>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<DetalleDescompuestoDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Crear ( [FromBody] DetalleDescompuestoCreacionDto detalleDto )
        {
            var resultado = await _service.CrearAsync(detalleDto);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // PUT: api/detalledescompuesto/{id}
        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Actualizar ( int id, [FromBody] DetalleDescompuestoEdicionDto detalleDto )
        {
            var resultado = await _service.ActualizarAsync(id, detalleDto);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // DELETE: api/detalledescompuesto/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)] // Por la regla de negocio
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Eliminar ( int id )
        {
            var resultado = await _service.EliminarAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }
    }
}
