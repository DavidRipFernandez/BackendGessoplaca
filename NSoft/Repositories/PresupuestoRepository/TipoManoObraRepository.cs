using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models.Presupuesto;
using NSoft.Repositories.PresupuestoRepository.IRepositories;

namespace NSoft.Repositories.PresupuestoRepository
{
    public class TipoManoObraRepository:ITipoManoObraRepository
    {
        private readonly ApplicationDbContext _context; // CHANGED: Ajusta al nombre real de tu DbContext

        public TipoManoObraRepository ( ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<List<TipoManoObra>> ListarPorEstadoAsync (bool estado)
        {
            return await _context.TipoManoObras
                .AsNoTracking() // ADDED
                .Where(x => x.Estado == estado)
                .ToListAsync();
        }


        public async Task<TipoManoObra?> ObtenerPorIdAsync ( int id )
        {
            return await _context.TipoManoObras
                .Include(mo => mo.ManoDeObras)
                .AsNoTracking()
                .FirstOrDefaultAsync(mo => mo.TipoManoObraId == id);
        }

        public async Task AgregarAsync ( TipoManoObra entity )
        {
            // ADDED: Mutación trackeada
            await _context.TipoManoObras.AddAsync(entity);
            await _context.SaveChangesAsync(); // fail-fast
        }
        
        public async Task ActualizarAsync ( TipoManoObra entity )
        {
            var exists = await _context.TipoManoObras.FindAsync(entity.TipoManoObraId);

            if (exists == null)
                throw new KeyNotFoundException($"No se encontró TipoManoObra con Id = {entity.TipoManoObraId}.");

            exists.Nombre = entity.Nombre;
            exists.FechaModificacion = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task CambiarEstadoAsync ( int id, bool estado )
        {
            var exists = await _context.TipoManoObras.FindAsync(id);

            if (exists == null)
                throw new KeyNotFoundException($"No se encontró TipoManoObra con Id = {id}.");

            exists.Estado = estado;
            exists.FechaModificacion = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
