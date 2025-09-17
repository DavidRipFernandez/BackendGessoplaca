using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.IServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")] // ADDED: documenta el tipo de salida
    public class CategoriaMaterialController : ControllerBase
    {
        private readonly ICategoriaMaterialService _categoriaService;

        public CategoriaMaterialController ( ICategoriaMaterialService categoriaService )
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("activos")]
        // ADDED: Swagger hints
        [ProducesResponseType(typeof(ApiResponse<List<CategoriaMaterialDto>>), StatusCodes.Status200OK)]     // ADDED
        [ProducesResponseType(typeof(ApiResponse<List<CategoriaMaterialDto>>), StatusCodes.Status500InternalServerError)] // ADDED
        public async Task<IActionResult> ObtenerActivos ()
        {
            var resultado = await _categoriaService.ObtenerActivosAsync();
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("eliminados")]
        [ProducesResponseType(typeof(ApiResponse<List<CategoriaMaterialDto>>), StatusCodes.Status200OK)]     // ADDED
        [ProducesResponseType(typeof(ApiResponse<List<CategoriaMaterialDto>>), StatusCodes.Status500InternalServerError)] // ADDED
        public async Task<IActionResult> ObtenerEliminados ()
        {
            var resultado = await _categoriaService.ObtenerEliminadosAsync();
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<CategoriaMaterialDto>), StatusCodes.Status200OK)]           // ADDED
        [ProducesResponseType(typeof(ApiResponse<CategoriaMaterialDto>), StatusCodes.Status404NotFound)]     // ADDED
        [ProducesResponseType(typeof(ApiResponse<CategoriaMaterialDto>), StatusCodes.Status500InternalServerError)] // ADDED
        public async Task<IActionResult> ObtenerPorId ( int id )
        {
            var resultado = await _categoriaService.ObtenerPorIdAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("con-materiales/{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<CategoriaMaterialDto>), StatusCodes.Status200OK)]           // ADDED
        [ProducesResponseType(typeof(ApiResponse<CategoriaMaterialDto>), StatusCodes.Status404NotFound)]     // ADDED
        [ProducesResponseType(typeof(ApiResponse<CategoriaMaterialDto>), StatusCodes.Status500InternalServerError)] // ADDED
        public async Task<IActionResult> ObtenerPorIdConMateriales ( int id )
        {
            var resultado = await _categoriaService.ObtenerPorIdConMaterialesAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPost]
        [Consumes("application/json")] // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]                           // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]                   // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)]                     // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]          // ADDED
        public async Task<IActionResult> Agregar ( [FromBody] CategoriaMaterialDto nuevaCategoria )
        {
            var resultado = await _categoriaService.AgregarAsync(nuevaCategoria);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPut]
        [Consumes("application/json")] // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]                           // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]                   // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]                     // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)]                     // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]          // ADDED
        public async Task<IActionResult> Actualizar ( [FromBody] CategoriaMaterialDto categoria )
        {
            var resultado = await _categoriaService.ActualizarAsync(categoria);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]                           // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]                   // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]                     // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)]                     // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]          // ADDED
        public async Task<IActionResult> Eliminar ( int id )
        {
            var resultado = await _categoriaService.EliminarAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPut("reactivar/{id:int}")]
        [Consumes("application/json")] // ADDED (aunque no hay body, mantiene consistencia en doc)
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]                           // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]                   // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]                     // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status409Conflict)]                     // ADDED
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]          // ADDED
        public async Task<IActionResult> Reactivar ( int id )
        {
            var resultado = await _categoriaService.ReactivarAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }
    }
}
