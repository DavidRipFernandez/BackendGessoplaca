using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.Models.Presupuesto;
using NSoft.Repositories.PresupuestoRepository.IRepositories;

namespace NSoft.Repositories.PresupuestoRepository
{
    public class PresupuestoRepository:IPresupuestoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PresupuestoRepository> _logger;

        public PresupuestoRepository ( ApplicationDbContext contexto, ILogger<PresupuestoRepository> logger )
        {
            _context = contexto;
            _logger = logger;
        }

        public async Task<IEnumerable<Presupuesto>> ListarPorEstadoAsync ( string? estado = null )
        {
            var consulta = _context.Presupuestos.AsNoTracking();

            if (!string.IsNullOrEmpty(estado))
            {
                consulta = consulta.Where(p => p.Estado == estado);
            }

            return await consulta.ToListAsync();
        }

        public async Task<Presupuesto?> ObtenerPorIdConDetallesAsync ( int presupuestoId )
        {
            return await _context.Presupuestos
                .Include(p => p.Descompuestos)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PresupuestoId == presupuestoId);
        }

        public async Task<Presupuesto> CrearAsync ( Presupuesto presupuesto )
        {
            await _context.Presupuestos.AddAsync(presupuesto);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Presupuesto {PresupuestoId} creado con referencia {Referencia}.", presupuesto.PresupuestoId, presupuesto.Referencia);

            return presupuesto;
        }

        public async Task ActualizarAsync ( Presupuesto presupuesto )
        {
            var existe = await _context.Presupuestos.AnyAsync(p => p.PresupuestoId == presupuesto.PresupuestoId);
            if (!existe)
            {
                throw new KeyNotFoundException($"No se puede actualizar porque no se encontró un presupuesto con el ID {presupuesto.PresupuestoId}.");
            }

            _context.Entry(presupuesto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Presupuesto {PresupuestoId} actualizado.", presupuesto.PresupuestoId);
        }

        public async Task EliminarAsync ( int presupuestoId )
        {
            var presupuesto = await _context.Presupuestos.FindAsync(presupuestoId);
            if (presupuesto == null)
            {
                throw new KeyNotFoundException($"No se puede eliminar porque no se encontró un presupuesto con el ID {presupuestoId}.");
            }

            _context.Presupuestos.Remove(presupuesto);
            await   _context.SaveChangesAsync();

            _logger.LogInformation("Presupuesto {PresupuestoId} marcado para eliminación.", presupuestoId);
        }

        public async Task<Presupuesto> ClonarAsync ( int presupuestoOriginalId, string nuevoNombreEmpresa )
        {
            _logger.LogInformation("Iniciando clonación profunda del presupuesto {PresupuestoId} para la empresa '{NuevaEmpresa}'.", presupuestoOriginalId, nuevoNombreEmpresa);

            var original = await _context.Presupuestos
                .Include(p => p.Descompuestos)
                    .ThenInclude(d => d.DetalleDescompuestos) 
                .Include(p => p.Descompuestos)
                    .ThenInclude(d => d.ManoDeObras)          
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PresupuestoId == presupuestoOriginalId);

            if (original == null)
            {
                throw new KeyNotFoundException($"No se puede clonar porque no se encontró el presupuesto original con ID {presupuestoOriginalId}.");
            }

            var clon = new Presupuesto
            {
                // Reseteamos datos del cliente y estado
                NombreEmpresa = nuevoNombreEmpresa,
                CIF = "",
                NombreContacto = "",
                Direccion = "",
                Poblacion = "",
                Provincia = "",
                CodigoPostal = "",
                Telefono = 0,
                Email = "",
                Estado = "Pendiente",
                FechaLimitePresentacion = DateTime.Now.AddDays(30),

                // Copiamos datos relevantes del presupuesto
                Referencia = $"COPIA - {original.Referencia}",
                TotalPresupuesto = original.TotalPresupuesto,

                // 2. Clonamos la jerarquía de forma anidada
                Descompuestos = original.Descompuestos.Select(d => new Descompuesto
                {
                    // Copiamos propiedades del Descompuesto
                    IsPlantilla = d.IsPlantilla,
                    Titulo = d.Titulo,
                    Descripcion = d.Descripcion,
                    UnidadMedida = d.UnidadMedida,
                    Precio = d.Precio,
                    ManoObra = d.ManoObra,
                    Beneficio = d.Beneficio,
                    GastoAdministrativo = d.GastoAdministrativo,
                    Estado = d.Estado, // Asumiendo que Descompuesto también tiene estado

                    // 2a. Clonamos los hijos de Descompuesto: DetalleDescompuesto
                    DetalleDescompuestos = d.DetalleDescompuestos.Select(det => new DetalleDescompuesto
                    {
                        NombreMaterial = det.NombreMaterial,
                        Proveedor = det.Proveedor,
                        Unidades = det.Unidades,
                        Precio = det.Precio,
                        Descuento = det.Descuento,
                        Estado = det.Estado
                    }).ToList(),

                    // 2b. Clonamos los hijos de Descompuesto: ManoDeObra
                    ManoDeObras = d.ManoDeObras.Select(mo => new ManoDeObra
                    {
                        Nombre = mo.Nombre,
                        UnidadesRealizar = mo.UnidadesRealizar,
                        Precio = mo.Precio,
                        TipoManoObraId = mo.TipoManoObraId 
                    }).ToList()

                }).ToList()
            };

            await _context.Presupuestos.AddAsync(clon);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Presupuesto original {OriginalId} clonado con éxito en el nuevo presupuesto {NuevoId}.", presupuestoOriginalId, clon.PresupuestoId);
            return clon;
        }
    }
}
