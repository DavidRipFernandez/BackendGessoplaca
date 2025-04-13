using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.IServices;

namespace NSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;

    public MarcaController ( IMarcaService marcaService )
    {
        _marcaService = marcaService;
    }

    [HttpGet("activos")]
    public async Task<IActionResult> ObtenerActivos ()
    {
        var response = await _marcaService.ObtenerActivosAsync();
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("eliminados")]
    public async Task<IActionResult> ObtenerEliminados ()
    {
        var response = await _marcaService.ObtenerEliminadosAsync();
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId ( int id )
    {
        var response = await _marcaService.ObtenerPorIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Agregar ( [FromBody] MarcaDto dto )
    {
        var response = await _marcaService.AgregarAsync(dto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut]
    public async Task<IActionResult> Actualizar ( [FromBody] MarcaDto dto )
    {
        var response = await _marcaService.ActualizarAsync(dto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar ( int id )
    {
        var response = await _marcaService.EliminarAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("reactivar/{id:int}")]
    public async Task<IActionResult> Reactivar ( int id )
    {
        var response = await _marcaService.ReactivarAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}
}
