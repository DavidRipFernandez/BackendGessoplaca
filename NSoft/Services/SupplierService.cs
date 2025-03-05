using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class SupplierService: ISupplierService
    {

        private readonly ISupplierRepository _supplierRepository;

        public SupplierService ( ISupplierRepository supplierRepository )
        {
            _supplierRepository = supplierRepository;
        }

        public async Task ActualizarAsync ( Proveedor supplier )
        {
            ArgumentNullException.ThrowIfNull(supplier);

            try
            {
                var actualizado = await _supplierRepository.ActualizarAsync(supplier);
                if (!actualizado)
                    throw new KeyNotFoundException($"No se encontró al proveedor con ID {supplier.ProveedorCifId} para actualizar");

            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar al proveedor.", ex);
            }
        }

        public async Task AgregarAsync ( Proveedor supplier )
        {

            try
            {
                await _supplierRepository.AgregarAsync(supplier);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar al proveedor.", ex);
            }

        }

        public async Task EliminarAsync ( string id )
        {
            try
            {
                var eliminado = await _supplierRepository.EliminarAsync(id);
                if (!eliminado)
                    throw new KeyNotFoundException($"No se encontró al proveedor con ID {id} para eliminar.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar al proveedor.", ex);
            }
        }

        public async Task<SupplierDTO?> ObtenerPorIdAsync ( string id )
        {
            try
            {
                var proveedor = await _supplierRepository.ObtenerPorIdAsync(id);
                if (proveedor == null)
                    return null;

                return new SupplierDTO
                {
                    ProveedorCifId = proveedor.ProveedorCifId,
                    Nombre = proveedor.Nombre,
                    DomicilioSocial = proveedor.DomicilioSocial,
                    Contactos = proveedor.Contactos.Select(c => new ContactoDTO
                    {
                        ContactoId = c.ContactoId,
                        Nombre = c.Nombre,
                        Correo = c.Correo,
                        Telefono = c.Telefono,
                        Descripcion = c.Descripcion
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener al proveedor.", ex);
            }
        }

        public async Task<IEnumerable<Proveedor>> ObtenerTodosAsync ()
        {
            try
            {
                return await _supplierRepository.ObtenerTodosAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de proveedores.", ex);
            }
        }

    }
}
