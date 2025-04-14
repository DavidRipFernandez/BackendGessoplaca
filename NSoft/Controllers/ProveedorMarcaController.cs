using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.IServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorMarcaController : ControllerBase
    {
        private readonly IProveedorMarcaService _proveedorMarcaService;

        public ProveedorMarcaController ( IProveedorMarcaService proveedorMarcaService )
        {
            _proveedorMarcaService = proveedorMarcaService;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar ( [FromBody] ProveedorMarcaDto proveedorMarca )
        {
            var resultado = await _proveedorMarcaService.AgregarAsync(proveedorMarca);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpDelete("{proveedorCifId}/{marcaId:int}")]
        public async Task<IActionResult> Eliminar ( string proveedorCifId, int marcaId )
        {
            var resultado = await _proveedorMarcaService.EliminarAsync(proveedorCifId, marcaId);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpPut("reactivar/{proveedorCifId}/{marcaId:int}")]
        public async Task<IActionResult> Reactivar ( string proveedorCifId, int marcaId )
        {
            var resultado = await _proveedorMarcaService.ReactivarAsync(proveedorCifId, marcaId);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("marcas-por-proveedor/{proveedorCifId}")]
        public async Task<IActionResult> ObtenerMarcasPorProveedor ( string proveedorCifId )
        {
            var resultado = await _proveedorMarcaService.ObtenerMarcasPorProveedorAsync(proveedorCifId);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("proveedores-por-marca/{marcaId:int}")]
        public async Task<IActionResult> ObtenerProveedoresPorMarca ( int marcaId )
        {
            var resultado = await _proveedorMarcaService.ObtenerProveedoresPorMarcaAsync(marcaId);
            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("eliminados")]
        public async Task<IActionResult> ObtenerRelacionesEliminadas ()
        {
            var resultado = await _proveedorMarcaService.ObtenerEliminadosAsync();
            return StatusCode(resultado.StatusCode, resultado);
        }
    }
}
