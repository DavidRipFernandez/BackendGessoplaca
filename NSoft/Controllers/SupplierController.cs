using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Models;
using NSoft.Services.IServices;

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

        [HttpGet("activos")]
        public async Task<IActionResult> ObtenerActivos ()
        {
            var response = await _supplierService.ObtenerActivosAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("eliminados")]
        public async Task<IActionResult> ObtenerEliminados ()
        {
            var response = await _supplierService.ObtenerEliminadosAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId ( string id )
        {
            var response = await _supplierService.ObtenerPorIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("con-marcas/{id}")]
        public async Task<IActionResult> ObtenerProveedorConMarcas ( string id )
        {
            var response = await _supplierService.ObtenerProveedorConMarcas(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar ( [FromBody] ProveedorDto proveedor )
        {
            var response = await _supplierService.AgregarAsync(proveedor);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar ([FromBody] ProveedorDto proveedor )
        {
            var response = await _supplierService.ActualizarAsync(proveedor);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar ( string id )
        {
            var response = await _supplierService.EliminarAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("reactivar/{id}")]
        public async Task<IActionResult> Reactivar ( string id )
        {
            var response = await _supplierService.ReactivarAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("asociar-marca")]
        public async Task<IActionResult> AgregarMarca ( [FromQuery] string proveedorId, [FromQuery] int marcaId )
        {
            var response = await _supplierService.AgregarMarcaAlProveedor(proveedorId, marcaId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("dar-baja-marca")]
        public async Task<IActionResult> DarBajaMarca ( [FromQuery] string proveedorId, [FromQuery] int marcaId )
        {
            var response = await _supplierService.DarBajaMarcaAlProveedor(proveedorId, marcaId);
            return StatusCode(response.StatusCode, response);
        }

    }
}
