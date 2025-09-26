using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models.Presupuesto;
using NSoft.Repositories.PresupuestoRepository.IRepositories;

namespace NSoft.Repositories.PresupuestoRepository
{
    public class DetalleDescompuestoRepository:IDetalleDescompuestoRepository
    {
        private readonly ApplicationDbContext _context;

        public DetalleDescompuestoRepository(ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<DetalleDescompuesto> ObtenerPorIdAsync( int detalleDescompuestoId )
        {
            var detalle = await _context.DetalleDescompuestos
                .AsNoTracking()
                .FirstOrDefaultAsync(detalle => detalle.DetalleDescompuestoId == detalleDescompuestoId);

            if ( detalle == null)
                throw new KeyNotFoundException($"No se encontró el detalle descompuesto con ID {detalleDescompuestoId} para eliminar.");

            return detalle;
        }

        public async Task AgregarAsync (DetalleDescompuesto detalle )
        {
            await _context.DetalleDescompuestos.AddAsync( detalle );
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(DetalleDescompuesto detalle )
        {
            var existente = await _context.DetalleDescompuestos.FindAsync( detalle.DetalleDescompuestoId );

            if (existente == null)
                throw new KeyNotFoundException($"No se encontro el DetalleDescompuesto con ID {detalle.DetalleDescompuestoId}.");

            // Mutación con entidad trackeada
            existente.NombreMaterial = detalle.NombreMaterial;
            existente.Proveedor = detalle.Proveedor;
            existente.Marca = detalle.Marca;
            existente.Unidades = detalle.Unidades;
            existente.Precio = detalle.Precio;
            existente.Descuento = detalle.Descuento;
            existente.Estado = detalle.Estado;
            existente.DescompuestoId = detalle.DescompuestoId;
            existente.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync(); // fail fast
        }

        public async Task EliminarAsync ( int detalleDescompuestoId )
        {
            var existente = await _context.DetalleDescompuestos.FindAsync(detalleDescompuestoId);
            if (existente == null)
                throw new KeyNotFoundException($"No se encontró el DetalleDescompuesto con ID {detalleDescompuestoId} para eliminar.");

            _context.DetalleDescompuestos.Remove(existente);
            await _context.SaveChangesAsync(); // fail fast
        }
    }
}
