using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models.Presupuesto;
using NSoft.Repositories.PresupuestoRepository.IRepositories;

namespace NSoft.Repositories.PresupuestoRepository
{
    public class ManoObraRepository: IManoObraRepository
    {
        private readonly ApplicationDbContext _context;

        public ManoObraRepository ( ApplicationDbContext context ) // ADDED
        {
            _context = context;
        }

        public async Task<ManoDeObra?> ObtenerPorIdConTipoAsync ( int manoObraId )
        {
            return await _context.ManoDeObras
                .Include(m => m.TipoManoObra)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ManoObraId == manoObraId);
        }

        public async Task<ManoDeObra> CrearAsync ( ManoDeObra manoDeObra )
        {
            await _context.ManoDeObras.AddAsync(manoDeObra);
            await _context.SaveChangesAsync();
            return manoDeObra;
        }

        public async Task ActualizarAsync ( ManoDeObra manoDeObra )
        {
            var existente = await _context.ManoDeObras.FindAsync(manoDeObra.ManoObraId);
            if (existente == null)
                throw new KeyNotFoundException($"No se encontró la mano de obra con ID {manoDeObra.ManoObraId}."); // ADDED

            existente.Nombre = manoDeObra.Nombre;
            existente.UnidadesRealizar = manoDeObra.UnidadesRealizar;
            existente.Precio = manoDeObra.Precio;
            existente.DescompuestoId = manoDeObra.DescompuestoId;
            existente.TipoManoObraId = manoDeObra.TipoManoObraId;
            existente.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync ( int manoObraId )
        {
            var existente = await _context.ManoDeObras.FindAsync(manoObraId);
            if (existente == null)
                throw new KeyNotFoundException($"No se encontró la mano de obra con ID {manoObraId} para eliminar."); // ADDED

            _context.ManoDeObras.Remove(existente);
            await _context.SaveChangesAsync();
        }
    }
}
