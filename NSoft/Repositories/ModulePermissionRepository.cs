using NSoft.Data;
using NSoft.DTOs;
using NSoft.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace NSoft.Repositories
{
    public class ModulePermissionRepository : IModulePermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public ModulePermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ListModulePermissionResponseDTO> GetModulePermissionByRoleAsync(int rolId)
        {
            try
            {
                // Se consulta la lista de módulos y se mapea a ModuloDTO, incluyendo el arreglo de permisos (IDs)
                var modules = await _context.Modulos
                    .Select(m => new ModulePermissionDTO
                    {
                        ModuloId = m.ModuloId,
                        ModuloCodigo = m.ModuloCodigo,
                        NombreModulo = m.NombreModulo,
                        Descripcion = m.Descripcion,
                        Permissions = m.RolesModulos
                            .Where(rm => rm.RolId == rolId)
                            .Select(rm => rm.TipoPermiso.TipoPermisoId)
                            .ToList()
                    })
                    .ToListAsync();

                return new ListModulePermissionResponseDTO { Modules = modules };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de módulos para el rol.", ex);
            }
        }
    }
}
