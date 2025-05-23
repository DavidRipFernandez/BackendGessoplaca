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

        [HttpGet()]
        public async Task<IActionResult> Obtener ()
        {
            var response = await _supplierService.ObtenerTodosAsync();
            return StatusCode(response.StatusCode, response);
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

        [HttpGet("detallado/{id}")]
        public async Task<IActionResult> ObtenerProveedorConTodo ( string id )
        {
            var response = await _supplierService.ObtenerProveedorConRelacionesAsync(id);
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
    }
}
