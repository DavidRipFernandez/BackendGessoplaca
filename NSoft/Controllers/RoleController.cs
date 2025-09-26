using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.IServices;

namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize]
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAllRolesAsync();
            return StatusCode(response.StatusCode, response);

        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreatedDTO dto)
        {
            if (dto == null)
            {
                var errorResponse = ApiResponse<RoleDTO>.ErrorResponse(
                    "La información es requerida.",
                    "El objeto enviado está vacío.",
                    400);
                return StatusCode(errorResponse.StatusCode, errorResponse);
            }

            // Validar que se envíe información obligatoria
            if (string.IsNullOrWhiteSpace(dto.NombreRol) || string.IsNullOrWhiteSpace(dto.Descripcion))
            {
                var errorResponse = ApiResponse<RoleDTO>.ErrorResponse(
                    "Datos inválidos.",
                    "El nombre del rol y la descripción son obligatorios.",
                    400);
                return StatusCode(errorResponse.StatusCode, errorResponse);
            }

            var response = await _roleService.CreateRoleAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

    }
}
