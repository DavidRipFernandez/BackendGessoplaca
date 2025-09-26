using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models.Presupuesto;
using NSoft.Repositories.PresupuestoRepository.IRepositories;

namespace NSoft.Repositories.PresupuestoRepository
{
    public class DescompuestoRepository : IDescompuestoRepository
    {
        private readonly ApplicationDbContext _context;

        
        public DescompuestoRepository ( ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<IEnumerable<Descompuesto>> ObtenerPlantillas ()
        {
            return await _context.Descompuestos
                .Where(d => d.IsPlantilla)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Descompuesto> AgregarAsync ( Descompuesto descompuesto )
        {
            await _context.Descompuestos.AddAsync(descompuesto);
            await _context.SaveChangesAsync();
            return descompuesto; 
        }

        public async Task ActualizarParcialAsync ( int descompuestoId, string titulo, string descripcion, string unidadMedida, decimal beneficio, decimal manoDeObra, decimal gastoAdministrativo )
        {
            var descompuesto = await _context.Descompuestos.FindAsync(descompuestoId);
            if (descompuesto == null)
                throw new KeyNotFoundException($"No se encontró el descompuesto con ID {descompuestoId}.");

            // Mutación con entidad trackeada
            descompuesto.Titulo = titulo;
            descompuesto.Descripcion = descripcion;
            descompuesto.UnidadMedida = unidadMedida;
            descompuesto.Beneficio = beneficio;
            descompuesto.ManoObra = manoDeObra;
            descompuesto.GastoAdministrativo = gastoAdministrativo;

            await _context.SaveChangesAsync();
        }

        public async Task ActualizarCompletoAsync ( Descompuesto descompuesto )
        {
            var existe = await _context.Descompuestos.AnyAsync(p => p.DescompuestoId == descompuesto.DescompuestoId);
            if (!existe)
                throw new KeyNotFoundException($"No se puede actualizar porque no se encontró un descompuesto con el ID {descompuesto.DescompuestoId}.");

            _context.Entry(descompuesto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Descompuesto>> BuscarPorNombreAsync ( string nombre )
        {
            
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El parámetro 'nombre' es obligatorio.");

            var searchTerm = $"%{nombre}";

            return await _context.Descompuestos
                .AsNoTracking()
                .Where(descompuesto => EF.Functions.Like(descompuesto.Titulo, searchTerm))
                .ToListAsync();
        }

        public async Task EliminarAsync ( int idDescompuesto )
        {
            var descompuesto = await _context.Descompuestos.FindAsync(idDescompuesto);
            if (descompuesto == null)
                throw new KeyNotFoundException($"No se encontró el descompuesto con ID {idDescompuesto} para eliminar."); // OK

            _context.Descompuestos.Remove(descompuesto);
            await _context.SaveChangesAsync();
        }

        public async Task<Descompuesto> ObtenerConDetallesAsync ( int idDescompuesto )
        {
            var descompuesto = await _context.Descompuestos
                .Include(d => d.DetalleDescompuestos)
                .Include(d => d.ManoDeObras)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DescompuestoId == idDescompuesto);

            if (descompuesto == null)
                throw new KeyNotFoundException($"No se encontró el descompuesto con ID {idDescompuesto} para eliminar.");

            return descompuesto;
        }
    }
}
