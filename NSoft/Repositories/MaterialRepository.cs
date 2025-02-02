using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;
using System.Collections.Generic;

namespace NSoft.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public MaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task ActualizarAsync(Material material)
        {
            throw new NotImplementedException();
        }

        public Task AgregarAsync(Material material)
        {
            throw new NotImplementedException();
        }

        public Task EliminarAsync(Material material)
        {
            throw new NotImplementedException();
        }

        public Task<Material> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Material>> ObtenerTodosAsync()
        {
            return await _context.Materiales.ToListAsync();
        }
    }
}
