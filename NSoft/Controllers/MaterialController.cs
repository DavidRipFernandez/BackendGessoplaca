using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> Get() => Ok(await _materialService.ObtenerTodosAsync());
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> Get(int id)
        {
            var material = await _materialService.ObtenerPorIdAsync(id);
            if (material == null) return NotFound();
            return Ok(material);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Material material)
        {
            await _materialService.AgregarAsync(material);
            return CreatedAtAction(nameof(Get), new { id = material.MaterialId }, material);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Material material)
        {
            if (id != material.MaterialId) return BadRequest();
            await _materialService.ActualizarAsync(material);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _materialService.EliminarAsync(id);
            return NoContent();
        }
    }
}
