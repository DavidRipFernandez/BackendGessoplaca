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

        private List<ProveedorDto> MapearAProveedorDto ( IEnumerable<Proveedor> proveedores )
        {
            return proveedores.Select(p => new ProveedorDto
            {
                ProveedorCifId = p.ProveedorCifId,
                Nombre = p.Nombre,
                DomicilioSocial = p.DomicilioSocial,
                Contactos = p.Contactos.Select(c => new ContactoDto
                {
                    ContactoId = c.ContactoId,
                    Nombre = c.Nombre,
                    Correo = c.Correo,
                    Telefono = c.Telefono,
                    Descripcion = c.Descripcion,
                    Estado = c.Estado
                }).ToList()
            }).ToList();
        }

        public async Task<ApiResponse<List<ProveedorDto>>> ObtenerActivosAsync ()
        {
            try
            {
                var proveedores = await _supplierRepository.ObtenerPorEstadoAsync(true);
                var listaproveedores = MapearAProveedorDto(proveedores);

                return ApiResponse<List<ProveedorDto>>.SuccessResponse(listaproveedores, "Proveedores activos obtenidos correctamente");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ProveedorDto>>.ErrorResponse(
                    "Ocurrio un error al obtener los proveedores",
                    ex.Message,500);
            }
        }

        public async Task<ApiResponse<List<ProveedorDto>>> ObtenerEliminadosAsync ()
        {
            try
            {
                var proveedores = await _supplierRepository.ObtenerPorEstadoAsync(false);
                var listaproveedores = MapearAProveedorDto(proveedores);

                return ApiResponse<List<ProveedorDto>>.SuccessResponse(listaproveedores,
                    "Proveedores eliminados obtenidos correctamente.");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ProveedorDto>>.ErrorResponse(
                    "Ocurrio un error al obtener los proveedores",
                    ex.Message, 500);
            }
        }

        public async Task<ApiResponse<ProveedorDto>> ObtenerPorIdAsync ( string id )
        {
            try
            {
                var proveedor = await _supplierRepository.ObtenerPorIdAsync(id);
                if (proveedor == null)
                    return ApiResponse<ProveedorDto>.ErrorResponse(
                        "Proveedor no encontrado.", $"No se encontró el proveedor con ID {id}.", 404);

                var resultado = new ProveedorDto
                {
                    ProveedorCifId = proveedor.ProveedorCifId,
                    Nombre = proveedor.Nombre,
                    DomicilioSocial = proveedor.DomicilioSocial,
                };

                return ApiResponse<ProveedorDto>.SuccessResponse(resultado,"Operacion exitosa");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProveedorDto>.ErrorResponse("Error al obtener proveedor.",
                    ex.Message, 500);
            }
        }

        public async Task<ApiResponse<ProveedorDto>> ObtenerProveedorConMarcas ( string id )
        {
            try
            {
                var proveedor = await _supplierRepository.ObtenerPorIdAsync(id);
                if (proveedor == null)
                    return ApiResponse<ProveedorDto>.ErrorResponse("Proveedor no encontrado.", $"No se encontró el proveedor con ID {id}.", 404);

                var resultado = new ProveedorDto
                {
                    ProveedorCifId = proveedor.ProveedorCifId,
                    Nombre = proveedor.Nombre,
                    DomicilioSocial = proveedor.DomicilioSocial,
                    Contactos = proveedor.Contactos.Select(c => new ContactoDto
                    {
                        ContactoId = c.ContactoId,
                        Nombre = c.Nombre,
                        Correo = c.Correo,
                        Telefono = c.Telefono,
                        Descripcion = c.Descripcion
                    }).ToList()
                };

                return ApiResponse<ProveedorDto>.SuccessResponse(resultado, "Operacion exitosa.");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProveedorDto>.ErrorResponse("Error al obtener proveedor con marcas.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> AgregarAsync ( ProveedorDto supplier )
        {
            try
            {
                var proveedor = new Proveedor
                {
                    ProveedorCifId = supplier.ProveedorCifId,
                    Nombre = supplier.Nombre,
                    DomicilioSocial = supplier.DomicilioSocial
                };

                var result = await _supplierRepository.AgregarAsync(proveedor);
                return result
                    ? ApiResponse<bool>.SuccessResponse(true, "Proveedor agregado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo agregar el proveedor.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al agregar proveedor.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync ( ProveedorDto supplier )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(supplier);

                var proveedor = new Proveedor
                {
                    ProveedorCifId = supplier.ProveedorCifId,
                    Nombre = supplier.Nombre,
                    DomicilioSocial = supplier.DomicilioSocial
                };

                var result = await _supplierRepository.ActualizarAsync(proveedor);
                return result
                    ? ApiResponse<bool>.SuccessResponse(true, "Proveedor actualizado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo actualizar el proveedor.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al actualizar proveedor.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync ( string id )
        {
            try
            {
                var result = await _supplierRepository.EliminarAsync(id);
                return result
                    ? ApiResponse<bool>.SuccessResponse(true, "Proveedor eliminado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo eliminar el proveedor.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al eliminar proveedor.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ReactivarAsync ( string id )
        {
            try
            {
                var result = await _supplierRepository.ReactivarAsync(id);
                return result
                    ? ApiResponse<bool>.SuccessResponse(true, "Proveedor reactivado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo reactivar el proveedor.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al reactivar proveedor.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> AgregarMarcaAlProveedor ( string proveedorId, int marcaId )
        {
            try
            {
                var result = await _supplierRepository.AgregarMarcaAlProveedor(proveedorId, marcaId);
                return result
                    ? ApiResponse<bool>.SuccessResponse(true, "Marca agregada al proveedor correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo agregar la marca al proveedor.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al agregar marca al proveedor.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> DarBajaMarcaAlProveedor ( string proveedorId, int marcaId )
        {
            try
            {
                var result = await _supplierRepository.DarBajaMarcaAlProveedor(proveedorId, marcaId);
                return result
                    ? ApiResponse<bool>.SuccessResponse(true, "Marca dada de baja correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo dar de baja la marca.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al dar de baja la marca al proveedor.", ex.Message, 500);
            }
        }

    }
}
