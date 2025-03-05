using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Contacto>>> Get ()
        {
            var contactos = await _contactService.ObtenerTodosAsync();
            return Ok(contactos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contacto>> Get ( int id )
        {
            var contacto = await _contactService.ObtenerPorIdAsync(id);
            if (contacto == null)
            {
                return NotFound($"No se encontró un contacto con ID {id}");
            }
            return Ok(contacto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post ( [FromBody] Contacto contacto )
        {
            if (contacto == null)
            {
                return BadRequest("El contacto no puede ser nulo.");
            }

            await _contactService.AgregarAsync(contacto);
            return CreatedAtAction(nameof(Get), new { id = contacto.ContactoId }, contacto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put ( int id, [FromBody] Contacto contacto )
        {
            if (contacto == null || id != contacto.ContactoId)
            {
                return BadRequest("El ID del contacto no coincide o el objeto es nulo.");
            }

            await _contactService.ActualizarAsync(contacto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete ( int id )
        {
            await _contactService.EliminarAsync(id);
            return NoContent();
        }

    }
}
