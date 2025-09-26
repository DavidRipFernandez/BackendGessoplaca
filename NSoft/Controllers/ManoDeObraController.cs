using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.PresupuestoService.IPresupuestoServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManoDeObraController : ControllerBase
    {
        private readonly IManoObraService _service;

        public ManoDeObraController ( IManoObraService service )
        {
            _service = service;
        }

        // GET: api/manodeobra/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<ManoDeObraDto>), StatusCodes.Status200OK)]                  // ADDED
        [ProducesResponseType(typeof(ApiResponse<ManoDeObraDto>), StatusCodes.Status404NotFound)]            // ADDED
        [ProducesResponseType(typeof(ApiResponse<ManoDeObraDto>), StatusCodes.Status400BadRequest)]          // ADDED
        [ProducesResponseType(typeof(ApiResponse<ManoDeObraDto>), StatusCodes.Status500InternalServerError)] // ADDED
        public async Task<IActionResult> ObtenerPorId ( int id )
        {
            var resultado = await _service.ObtenerPorIdAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // POST: api/manodeobra
        [HttpPost]
        [Consumes("application/json")] // ADDED
        [ProducesResponseType(typeof(ApiResponse<ManoDeObraDto>), StatusCodes.Status200OK)]                  // ADDED
        [ProducesResponseType(typeof(ApiResponse<ManoDeObraDto>), StatusCodes.Status400BadRequest)]          // ADDED
        [ProducesResponseType(typeof(ApiResponse<ManoDeObraDto>), StatusCodes.Status409Conflict)]            // ADDED
        [ProducesResponseType(typeof(ApiResponse<ManoDeObraDto>), StatusCodes.Status500InternalServerError)] // ADDED
        public async Task<IActionResult> Crear ( [FromBody] ManoDeObraCreacionDto dto )
        {
            var resultado = await _service.CrearAsync(dto);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // PUT: api/manodeobra/{id}
        [HttpPut("{id:int}")]
        [Consumes("application/json")] // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]                           // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]                   // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]                     // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)]                     // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]          // ADDED
        public async Task<IActionResult> Actualizar ( int id, [FromBody] ManoDeObraEdicionDto dto )
        {
            var resultado = await _service.ActualizarAsync(id, dto);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // DELETE: api/manodeobra/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]                           // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]                   // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]                     // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]          // ADDED
        public async Task<IActionResult> Eliminar ( int id )
        {
            var resultado = await _service.EliminarAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }
    }
}
