using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.IServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulePermissionController : ControllerBase
    {
        private readonly IModulePermissionService _modulePermissionService;

        public ModulePermissionController(IModulePermissionService modulePermissionService)
        {
            _modulePermissionService = modulePermissionService;
        }
        [HttpGet("PermissionByRol")]
        public async Task<IActionResult> GetModulePermissionByRole([FromQuery] int rolId)
        {
            if (rolId <= 0)
            {
                var errorResponse = ApiResponse<ListModulePermissionResponseDTO>.ErrorResponse(
                    "El rolId debe ser mayor a 0.",
                    "Valor inválido para rolId.",
                    400);
                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
            var response = await _modulePermissionService.GetModulePermissionsByRoleAsync(rolId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
