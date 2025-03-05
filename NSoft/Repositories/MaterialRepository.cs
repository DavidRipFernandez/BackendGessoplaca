using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
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

        public async Task ActualizarAsync(Material material)
        {
            _context.Materiales.Update(material);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarAsync(Material material)
        {
            await _context.Materiales.AddAsync(material);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var material = await _context.Materiales.FindAsync(id);
            if (material!=null)
            {
                _context.Materiales.Remove(material);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Material> ObtenerPorIdAsync(int id)
        {
            return await _context.Materiales.FindAsync(id);        
        }

        public async Task<IEnumerable<Material>> ObtenerTodosAsync()
        {
            return await _context.Materiales.ToListAsync();
        }
    }
}
