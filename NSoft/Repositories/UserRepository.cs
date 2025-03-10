using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;

namespace NSoft.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        //private readonly ILogger<UsuarioRepository> _logger;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync()
        {
            try
            {
                var usuarios = await _context.Usuarios
                       .Include(u => u.Rol) // Incluimos la relación con Rol
                       .Select(u => new UsuarioDTO
                       {
                           UsuarioId = u.UsuarioId,
                           Nombre = u.Nombre,
                           Constraseña = u.ContraseñaHash,
                           Correo = u.Correo,
                           Estado = u.Estado,
                           RolId = u.Rol.RolId, // RolId en nivel principal
                           RolNombre = u.Rol.NombreRol // Nombre del rol en nivel principal
                       })
                       .ToListAsync();
                return usuarios?? new List<UsuarioDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de usuarios", ex);
            }

        }
    }
}



