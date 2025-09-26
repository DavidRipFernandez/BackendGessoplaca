using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.DTOs.NSoft.DTOs.Presupuesto;
using NSoft.Services.PresupuestoService.IPresupuestoServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoManoObraController : ControllerBase
    {

        private readonly ITipoManoObraService _service;

        public TipoManoObraController ( ITipoManoObraService service )
        {
            _service = service;
        }

        [HttpGet("activos")]
        [ProducesResponseType(typeof(ApiResponse<List<TipoManoObraDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<TipoManoObraDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerActivos ()
        {
            var resultado = await _service.ListarActivosAsync();
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("eliminados")] // CHANGED: alias "eliminados" para inactivos (Estado=false)
        [ProducesResponseType(typeof(ApiResponse<List<TipoManoObraDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<TipoManoObraDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerEliminados ()
        {
            var resultado = await _service.ListarInactivosAsync();
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDetalleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDetalleDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDetalleDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerPorId ( int id )
        {
            var resultado = await _service.ObtenerPorIdAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPost]
        [Consumes("application/json")] // ADDED
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDto>), StatusCodes.Status200OK)] // CHANGED: devolvemos el DTO creado
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDto>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Agregar ( [FromBody] TipoManoObraCreateDto nuevo )
        {
            var resultado = await _service.AgregarAsync(nuevo);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // CHANGED: a diferencia de tu ejemplo, nuestro Service espera {id} en ruta
        [HttpPut("{id:int}")]
        [Consumes("application/json")] // ADDED
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDto>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<TipoManoObraDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Actualizar ( int id, [FromBody] TipoManoObraUpdateDto dto )
        {
            var resultado = await _service.ActualizarAsync(id, dto);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Eliminar ( int id )
        {
            var resultado = await _service.EliminarAsync(id); // soft delete (Estado=false)
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPut("reactivar/{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Reactivar(int id)
        {
            var resultado = await _service.ReactivarAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }
    }
}
