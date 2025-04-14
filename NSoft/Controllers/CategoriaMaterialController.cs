using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.IServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaMaterialController : ControllerBase
    {
        private readonly ICategoriaMaterialService _categoriaService;

        public CategoriaMaterialController ( ICategoriaMaterialService categoriaService )
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("activos")]
        public async Task<IActionResult> ObtenerActivos ()
        {
            var resultado = await _categoriaService.ObtenerActivosAsync();
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("eliminados")]
        public async Task<IActionResult> ObtenerEliminados ()
        {
            var resultado = await _categoriaService.ObtenerEliminadosAsync();
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId ( int id )
        {
            var resultado = await _categoriaService.ObtenerPorIdAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("con-materiales/{id:int}")]
        public async Task<IActionResult> ObtenerPorIdConMateriales ( int id )
        {
            var resultado = await _categoriaService.ObtenerPorIdConMaterialesAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar ( [FromBody] CategoriaMaterialDto nuevaCategoria )
        {
            var resultado = await _categoriaService.AgregarAsync(nuevaCategoria);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar ( [FromBody] CategoriaMaterialDto categoria )
        {
            var resultado = await _categoriaService.ActualizarAsync(categoria);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar ( int id )
        {
            var resultado = await _categoriaService.EliminarAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPut("reactivar/{id:int}")]
        public async Task<IActionResult> Reactivar ( int id )
        {
            var resultado = await _categoriaService.ReactivarAsync(id);
            return StatusCode(resultado.StatusCode, resultado);
        }
    }
}
