using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSoft.DTOs;
using NSoft.Services.IServices;
using System.Runtime.InteropServices;


namespace NSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usuarioService;

        public UserController(IUserService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var response = await _usuarioService.GetAllUsuariosAsync();
            return StatusCode(response.StatusCode, response);
        }
        // PUT: api/User
        [HttpPut("UpdateUsers")]
        public async Task<IActionResult> UpdateUsuario([FromBody] UserUpdateDTO UserUpdate)
        {
            var response = await _usuarioService.UpdateUsuarioAsync(UserUpdate);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var response = await _usuarioService.DeleteUserAsync(userId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
