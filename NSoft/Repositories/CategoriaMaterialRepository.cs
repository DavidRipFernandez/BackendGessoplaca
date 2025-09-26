using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models;
using NSoft.Repositories.IRepositories;

namespace NSoft.Repositories
{
    public class CategoriaMaterialRepository : ICategoriaMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaMaterialRepository ( ApplicationDbContext context ) 
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriaMaterial>> ObtenerPorEstadoAsync ( bool estado )
        {
            
            return await _context.CategoriasMateriales
                .AsNoTracking()
                .Where(c => c.Estado == estado)
                .ToListAsync();
        }

        public async Task<CategoriaMaterial?> ObtenerPorIdAsync ( int id )
        {
            
            return await _context.CategoriasMateriales
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoriaId == id);
        }

        public async Task<CategoriaMaterial?> ObtenerPorIdConMaterialesAsync ( int id )
        {
            
            return await _context.CategoriasMateriales
                .Include(c => c.Materiales)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoriaId == id);
        }

        
        public async Task AgregarAsync ( CategoriaMaterial categoria ) 
        {
            await _context.CategoriasMateriales.AddAsync(categoria);
            await _context.SaveChangesAsync(); 
        }

        public async Task ActualizarAsync ( CategoriaMaterial categoria ) 
        {
            var existente = await _context.CategoriasMateriales.FindAsync(categoria.CategoriaId);
            if (existente == null)
                throw new KeyNotFoundException($"No se encontró la categoría con ID {categoria.CategoriaId}"); 

            
            existente.Nombre = categoria.Nombre;
            existente.Descripcion = categoria.Descripcion;
            existente.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task CambiarEstadoAsync ( int id, bool estado ) 
        {
            var categoria = await _context.CategoriasMateriales.FindAsync(id)
                           ?? throw new KeyNotFoundException($"No se encontró la categoría con ID {id}");

            categoria.Estado = estado;
            categoria.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync(); 
        }
    }
}
