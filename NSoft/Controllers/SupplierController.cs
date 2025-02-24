using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.Models;
using NSoft.Services;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController ( ISupplierService supplierService )
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Proveedor>>> Get ()
        {
            var contactos = await _supplierService.ObtenerTodosAsync();
            return Ok(contactos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Proveedor>> Get ( string id )
        {
            var supplier = await _supplierService.ObtenerPorIdAsync(id);
            if (supplier == null)
            {
                return NotFound($"No se encontró un proveedor con ID {id}");
            }
            return Ok(supplier);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post ( [FromBody] Proveedor supplier )
        {
            if (supplier == null)
            {
                return BadRequest("El contacto no puede ser nulo.");
            }

            await _supplierService.AgregarAsync(supplier);
            return CreatedAtAction(nameof(Get), new { id = supplier.ProveedorCifId }, supplier);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put ( string id, [FromBody] Proveedor supplier )
        {
            if (supplier == null)
            {
                return BadRequest("El ID del contacto no coincide o el objeto es nulo.");
            }

            await _supplierService.ActualizarAsync(supplier);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete ( string id )
        {
            await _supplierService.EliminarAsync(id);
            return NoContent();
        }

    }
}
