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

        public async Task<ApiResponse<ProveedorDto>> ObtenerProveedorConRelacionesAsync ( string id )
        {
            try
            {
                var proveedor = await _supplierRepository.ObtenerProveedorConRelacionesAsync(id);
                if (proveedor is null)
                    return ApiResponse<ProveedorDto>.ErrorResponse(
                        "Proveedor no encontrado.", $"No se encontró el proveedor con ID {id}.", 404);

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
                    }).ToList(),
                    Marcas = proveedor.ProveedoresMarcas.Where(pm => pm.Marca != null).Select(pm => new MarcaDto
                    {
                        MarcaId = pm.Marca.MarcaId,
                        Nombre = pm.Marca.Nombre,
                        Descripcion = pm.Marca.Descripcion,
                        Estado = pm.Marca.Estado
                    }).ToList()
                };

                return ApiResponse<ProveedorDto>.SuccessResponse(resultado,"Operacion exitosa");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProveedorDto>.ErrorResponse("Error al obtener proveedor.",
                    ex.Message, 500);
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
                var result = await _supplierRepository.CambiarEstadoAsync(id,false);
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
                var result = await _supplierRepository.CambiarEstadoAsync(id, true);
                return result
                    ? ApiResponse<bool>.SuccessResponse(true, "Proveedor reactivado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo reactivar el proveedor.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al reactivar proveedor.", ex.Message, 500);
            }
        }
    }
}
