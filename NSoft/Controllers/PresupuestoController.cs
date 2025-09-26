using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSoft.DTOs;
using NSoft.Services.PresupuestoService.IPresupuestoService;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoService _servicio;

        public PresupuestoController ( IPresupuestoService servicio )
        {
            _servicio = servicio;
        }

        [HttpGet("pendientes")]
        public async Task<IActionResult> ListarPendientes ()
        {
            var response = await _servicio.ListarPendientesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("en-curso")]
        public async Task<IActionResult> ListarEnCurso ()
        {
            var response = await _servicio.ListarEnCursoAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("rechazados")]
        public async Task<IActionResult> ListarRechazados ()
        {
            var response = await _servicio.ListarRechazadosAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId ( int id )
        {
            var response = await _servicio.ObtenerPorIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Crear ( [FromBody] PresupuestoDto presupuestoDto )
        {
            var response = await _servicio.CrearAsync(presupuestoDto);
            // Para un 201 Created, también podrías devolver la URL del nuevo recurso si lo deseas
            if (response.Success)
            {
                return CreatedAtAction(nameof(ObtenerPorId), new { id = response.Data.PresupuestoId }, response);
            }
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar ( int id, [FromBody] PresupuestoDto presupuestoDto )
        {
            var response = await _servicio.ActualizarAsync(id, presupuestoDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar ( int id )
        {
            var response = await _servicio.EliminarAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("{id}/clonar")]
        public async Task<IActionResult> Clonar ( int id, [FromBody] string nombreEmpresa )
        {
            var response = await _servicio.ClonarAsync(id, nombreEmpresa);
            if (response.Success)
            {
                return CreatedAtAction(nameof(ObtenerPorId), new { id = response.Data.PresupuestoId }, response);
            }
            return StatusCode(response.StatusCode, response);
        }
    }
}
