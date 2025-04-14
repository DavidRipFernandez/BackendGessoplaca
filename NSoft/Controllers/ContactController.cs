using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Models;
using NSoft.Services.IServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly IContactService _contactService;

        public ContactController (IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("activos")]
        public async Task<IActionResult> ObtenerActivos ()
        {
            var response = await _contactService.ObtenerActivosPorProveedorAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("eliminados")]
        public async Task<IActionResult> ObtenerEliminados ()
        {
            var response = await _contactService.ObtenerEliminadosPorProveedorAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorNombre ( [FromQuery] string nombre )
        {
            var response = await _contactService.ObtenerPorNombreAsync(nombre);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId ( int id )
        {
            var response = await _contactService.ObtenerPorIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar ( [FromBody] ContactoDto dto )
        {
            var response = await _contactService.AgregarAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar ( [FromBody] ContactoDto dto )
        {
            var response = await _contactService.ActualizarAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar ( int id )
        {
            var response = await _contactService.EliminarAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("reactivar/{id:int}")]
        public async Task<IActionResult> Reactivar ( int id )
        {
            var response = await _contactService.ReactivarAsync(id);
            return StatusCode(response.StatusCode, response);
        }

    }
}
