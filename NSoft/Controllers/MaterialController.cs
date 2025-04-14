using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Models;
using NSoft.Services.IServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet("activos")]
        public async Task<IActionResult> ObtenerActivos ()
        {
            var response = await _materialService.ObtenerActivosAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("eliminados")]
        public async Task<IActionResult> ObtenerEliminados ()
        {
            var response = await _materialService.ObtenerEliminadosAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId ( int id )
        {
            var response = await _materialService.ObtenerPorIdConCategoriaAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar ( [FromBody] MaterialDto material )
        {
            var response = await _materialService.AgregarAsync(material);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar ( [FromBody] MaterialDto material )
        {
            var response = await _materialService.ActualizarAsync(material);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar ( int id )
        {
            var response = await _materialService.EliminarAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("reactivar/{id:int}")]
        public async Task<IActionResult> Reactivar ( int id )
        {
            var response = await _materialService.ReactivarAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
