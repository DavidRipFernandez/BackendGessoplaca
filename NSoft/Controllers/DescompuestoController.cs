using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.PresupuestoService.IPresupuestoServices;

namespace NSoft.Controllers.Presupuesto
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")] 
    public class DescompuestoController : ControllerBase
    {
        private readonly IDescompuestoService _service;

        public DescompuestoController ( IDescompuestoService service )
        {
            _service = service;
        }

        // GET: api/descompuesto/plantillas
        [HttpGet("plantillas")]
        [ProducesResponseType(typeof(ApiResponse<List<DescompuestoDto>>), StatusCodes.Status200OK)]     // ADDED
        [ProducesResponseType(typeof(ApiResponse<List<DescompuestoDto>>), StatusCodes.Status500InternalServerError)] // ADDED
        public async Task<IActionResult> ObtenerPlantillas ()
        {
            var resultado = await _service.ObtenerPlantillasAsync();
            return StatusCode(resultado.StatusCode, resultado);
        }

        // GET: api/descompuesto/buscar?nombre=xxx
        [HttpGet("buscar")]
        [ProducesResponseType(typeof(ApiResponse<List<DescompuestoDto>>), StatusCodes.Status200OK)]     // ADDED
        [ProducesResponseType(typeof(ApiResponse<List<DescompuestoDto>>), StatusCodes.Status400BadRequest)] // ADDED
        [ProducesResponseType(typeof(ApiResponse<List<DescompuestoDto>>), StatusCodes.Status500InternalServerError)] // ADDED
        public async Task<IActionResult> BuscarPorNombre ( [FromQuery] string nombre )
        {
            var resultado = await _service.BuscarPorNombreAsync(nombre);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // GET: api/descompuesto/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<DescompuestoDto>), StatusCodes.Status200OK)]           // ADDED
        [ProducesResponseType(typeof(ApiResponse<DescompuestoDto>), StatusCodes.Status404NotFound)]     // ADDED
        [ProducesResponseType(typeof(ApiResponse<DescompuestoDto>), StatusCodes.Status400BadRequest)]   // ADDED
        [ProducesResponseType(typeof(ApiResponse<DescompuestoDto>), StatusCodes.Status500InternalServerError)] // ADDED
        public async Task<IActionResult> ObtenerConDetalles ( int id )
        {
            var resultado = await _service.ObtenerConDetallesAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // POST: api/descompuesto
        [HttpPost]
        [Consumes("application/json")] // ADDED
        [ProducesResponseType(typeof(ApiResponse<DescompuestoDto>), StatusCodes.Status200OK)]           // ADDED
        [ProducesResponseType(typeof(ApiResponse<DescompuestoDto>), StatusCodes.Status400BadRequest)]   // ADDED
        [ProducesResponseType(typeof(ApiResponse<DescompuestoDto>), StatusCodes.Status409Conflict)]     // ADDED
        [ProducesResponseType(typeof(ApiResponse<DescompuestoDto>), StatusCodes.Status500InternalServerError)] // ADDED
        public async Task<IActionResult> Crear ( [FromBody] DescompuestoCreacionDto dto )
        {
            var resultado = await _service.CrearAsync(dto);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // PATCH: api/descompuesto/{id}
        [HttpPatch("{id:int}")]
        [Consumes("application/json")] // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]                      // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]              // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]                // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)]                // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]     // ADDED
        public async Task<IActionResult> ActualizarParcial ( int id, [FromBody] DescompuestoEdicionParcialDto dto )
        {
            var resultado = await _service.ActualizarParcialAsync(id, dto);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // PUT: api/descompuesto/{id}
        [HttpPut("{id:int}")]
        [Consumes("application/json")] // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]                      // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]              // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]                // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)]                // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]     // ADDED
        public async Task<IActionResult> ActualizarCompleto ( int id, [FromBody] DescompuestoDto dto )
        {
            var resultado = await _service.ActualizarCompletoAsync(id, dto);
            return StatusCode(resultado.StatusCode, resultado);
        }

        // DELETE: api/descompuesto/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]                      // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]              // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]                // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]     // ADDED
        public async Task<IActionResult> Eliminar ( int id )
        {
            var resultado = await _service.EliminarAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }
    }
}
