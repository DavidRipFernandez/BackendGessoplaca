using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.DTOs;
using NSoft.Models;
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

        public async Task<RoleDTO> CreateRoleAsync(RoleCreatedDTO dto)
        {
            try
            {
                // Mapear el DTO a la entidad Rol.
                var newRole = new Rol
                {
                    NombreRol = dto.NombreRol,
                    Descripcion = dto.Descripcion,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                // Agregar la entidad al contexto.
                await _context.Roles.AddAsync(newRole);
                await _context.SaveChangesAsync();

                // Mapear la entidad creada a RoleDTO.
                var roleDto = new RoleDTO
                {
                    RolId = newRole.RolId,
                    NombreRol = newRole.NombreRol,
                    Descripcion = newRole.Descripcion,
                    Estado = newRole.Estado
                };

                return roleDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el nuevo rol.", ex);
            }
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
                    Descripcion = r.Descripcion,
                    Estado = r.Estado
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
