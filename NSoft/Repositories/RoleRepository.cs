using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.DTOs;
using NSoft.Repositories.IRepositories;

namespace NSoft.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _context.Roles
                    .Select(r => new RoleDTO
                {
                    RolId =r.RolId,
                    NombreRol = r.NombreRol,
                    Descripcion = r.Descripcion
                }).ToListAsync();

                return roles;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de roles", ex);
            }
        }

        public async Task<bool> RoleExistsAsync(int rolId)
        {
            return await _context.Roles.AnyAsync(r => r.RolId == rolId);
        }
    }
}
