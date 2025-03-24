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
                           Contraseña = u.ContraseñaHash,
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
        public async Task<UsuarioDTO> UpdateUsuarioAsync(UserUpdateDTO UserUpdateDto)
        {
            try
            {
                // Buscar el usuario por su ID
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == UserUpdateDto.UsuarioId);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado.");
                }

                // Actualizar los campos (si la contraseña se envía, se actualiza; de lo contrario se deja la existente)
                usuario.Nombre = UserUpdateDto.Nombre;
                usuario.Correo = UserUpdateDto.Correo;
                usuario.Estado = UserUpdateDto.Estado;
                usuario.RolId = UserUpdateDto.RolId;
                usuario.ContraseñaHash = BCrypt.Net.BCrypt.HashPassword(UserUpdateDto.Contraseña);
                

                // Actualizamos el SecurityStamp para reflejar el cambio (opcional)
                usuario.SecurityStamp = Guid.NewGuid().ToString();

                // Guardar cambios
                await _context.SaveChangesAsync();

                // Mapear la entidad actualizada a DTO
                var updatedUsuario = new UsuarioDTO
                {
                    UsuarioId = usuario.UsuarioId,
                    Nombre = usuario.Nombre,
                    Contraseña = usuario.ContraseñaHash,
                    Correo = usuario.Correo,
                    Estado = usuario.Estado,
                    RolId = usuario.RolId,
                    RolNombre = usuario.Rol != null ? usuario.Rol.NombreRol : string.Empty
                };

                return updatedUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario.", ex);
            }
        }

        public async Task<bool> UserExistAsync(int userId)
        {
            try
            {
                return await _context.Usuarios.AnyAsync(r => r.UsuarioId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al encontrar al usuario", ex);
            }
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == userId);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado.");
                }
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario.", ex);
            }
        }


    }
}



