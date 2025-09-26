using Microsoft.EntityFrameworkCore;
using NSoft.Data;
using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;

namespace NSoft.Repositories
{
    public class PrecioTarifaRepository : IPrecioTarifaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PrecioTarifaRepository> _logger;

        public PrecioTarifaRepository ( ApplicationDbContext context, ILogger<PrecioTarifaRepository> logger )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> GuardarPrecioAsync ( PrecioTarifa precio )
        {
            try
            {
                var existente = await _context.PreciosTarifas.FirstOrDefaultAsync(p =>
                    p.MaterialId == precio.MaterialId &&
                    p.MarcaId == precio.MarcaId &&
                    p.ProveedorCifId == precio.ProveedorCifId);

                if (existente is not null)
                {
                    existente.Precio = precio.Precio;
                    existente.FechaModificacion = DateTime.UtcNow;
                }
                else
                {
                    await _context.PreciosTarifas.AddAsync(precio);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al guardar el precio.");
                throw new Exception("Error al guardar el precio.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al guardar el precio.");
                throw new Exception("Error inesperado al guardar el precio.", ex);
            }
        }

        public async Task<bool> GuardarMasivoAsync ( List<PrecioTarifa> precios )
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var precio in precios)
                {
                    await GuardarPrecioAsync(precio);
                }

                await transaction.CommitAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                _logger.LogError(dbEx, "Error en carga masiva.");
                throw new Exception("Error en carga masiva de precios.", dbEx);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error inesperado en carga masiva.");
                throw new Exception("Error inesperado al guardar precios.", ex);
            }
        }

        public async Task<IEnumerable<PrecioTarifaResumenDto>> ObtenerPreciosPorProveedorAsync ( string proveedorCifId )
        {
            try
            {
                return await _context.PreciosTarifas
                    .Where(p => p.ProveedorCifId == proveedorCifId)
                    .Select(p => new PrecioTarifaResumenDto
                    {
                        MaterialId = p.MaterialId,
                        NombreMaterial = p.Material.Nombre,
                        CodigoMaterial = p.Material.CodigoMaterial,
                        SistemaMedicion = p.Material.SistemaMedicion,
                        Precio = p.Precio,
                        FechaCreacion = p.FechaCreacion,
                        FechaModificacion = p.FechaModificacion,
                        MarcaId = p.ProveedorMarca.Marca.MarcaId,
                        NombreMarca = p.ProveedorMarca.Marca.Nombre,
                        ProveedorCifId = p.ProveedorMarca.Proveedor.ProveedorCifId,
                        NombreProveedor = p.ProveedorMarca.Proveedor.Nombre
                    })
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener precios por proveedor.");
                throw new Exception("Error al obtener precios por proveedor.", ex);
            }
        }

        public async Task<IEnumerable<PrecioTarifaResumenDto>> ObtenerPreciosPorMarcaAsync ( int marcaId )
        {
            try
            {
                return await _context.PreciosTarifas
                    .Where(p => p.MarcaId == marcaId)
                    .Select(p => new PrecioTarifaResumenDto
                    {
                        MaterialId = p.MaterialId,
                        NombreMaterial = p.Material.Nombre,
                        CodigoMaterial = p.Material.CodigoMaterial,
                        SistemaMedicion = p.Material.SistemaMedicion,
                        Precio = p.Precio,
                        FechaCreacion = p.FechaCreacion,
                        FechaModificacion = p.FechaModificacion,
                        MarcaId = p.ProveedorMarca.Marca.MarcaId,
                        NombreMarca = p.ProveedorMarca.Marca.Nombre,
                        ProveedorCifId = p.ProveedorMarca.Proveedor.ProveedorCifId,
                        NombreProveedor = p.ProveedorMarca.Proveedor.Nombre
                    })
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener precios por marca.");
                throw new Exception("Error al obtener precios por marca.", ex);
            }
        }

        public async Task<PrecioTarifa?> ObtenerPrecioMasBajoAsync ( int materialId )
        {
            try
            {
                return await _context.PreciosTarifas
                    .Include(p => p.Material)
                    .Include(p => p.ProveedorMarca).ThenInclude(pm => pm.Proveedor)
                    .Include(p => p.ProveedorMarca).ThenInclude(pm => pm.Marca)
                    .Where(p => p.MaterialId == materialId && p.Estado)
                    .OrderBy(p => p.Precio)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el precio más bajo para el material con ID {MaterialId}.", materialId);
                throw new Exception("Error al obtener el precio más bajo.", ex);
            }
        }

        public async Task<bool> ExisteAsync ( int materialId, string proveedorCifId, int marcaId )
        {
            try
            {
                return await _context.PreciosTarifas.AnyAsync(p =>
                    p.MaterialId == materialId &&
                    p.MarcaId == marcaId &&
                    p.ProveedorCifId == proveedorCifId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia del precio.");
                throw new Exception("Error al verificar existencia del precio.", ex);
            }
        }
    }
}
