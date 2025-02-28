using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using NSoft.Data;
using NSoft.Services;
using NSoft.DTOs;
using NSoft.Models;
using System.Text;

namespace NSoft.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var reponse = await _authService.LoginAsync(loginDto);
            return StatusCode(reponse.StatusCode, reponse);
        }
        //[Authorize]
        [HttpGet("permisos")]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new ApiResponse<string>("Usuario no autenticado", "", null, 401, false));
            }
            int userId = int.Parse(userIdClaim);
            var response = await _authService.ObtenerPermisosAsync(userId);

            return StatusCode(response.StatusCode, response);
        }
        //[Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var response = await _authService.ChangePasswordAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var response = await _authService.RegisterAsync(registerDto);   
            return StatusCode(response.StatusCode, response);
        }
    }
}
