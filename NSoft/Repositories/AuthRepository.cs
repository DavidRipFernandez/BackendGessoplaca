using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;
using NSoft.DTOs;

namespace NSoft.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<PermisoDto>> ObtenerPermisosPorUsuarioAsync(int userId)
        {
            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario == null || !usuario.Estado)
                return null;

            return await _context.RolesModulos
                .Where(rm => rm.RolId == usuario.RolId)
                .Select(rm => new PermisoDto
                {
                    Modulo = rm.Modulo.ModuloCodigo,
                    Permiso = rm.TipoPermiso.NombrePermiso
                })
                .ToListAsync();
        }

        public async Task<Usuario> ObtenerUsuarioPorCorreoAsync(string correo)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<Usuario> ObtenerUsuarioPorIdAsync(int userId)
        {
            return await _context.Usuarios.FindAsync(userId);
        }

        public async Task RegistrarUsuarioAsync(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario : {ex.Message}");
                throw new Exception("Error en la base de datos al registrar usuario.", ex);
            }
        }

        public async Task<bool> UsuarioExisteAsync(string correo)
        {
            return await _context.Usuarios.AnyAsync(u => u.Correo == correo);
        }
    }
}
