using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ISupplierRepository _supplierRepository;

        public ContactService( IContactRepository contactRepository, ISupplierRepository supplierRepository )
        {
            _contactRepository = contactRepository;
            _supplierRepository = supplierRepository;
        }

        private List<ContactoDto> MappearAContactoDto (IEnumerable<Contacto> contactos)
        {
            return contactos.Select( contacto => new ContactoDto
            {
                ContactoId = contacto.ContactoId,
                Nombre = contacto.Nombre,
                Descripcion = contacto.Descripcion,
                Correo = contacto.Correo,
                Telefono = contacto.Telefono,
                Estado = contacto.Estado,
                Proveedor = contacto.Proveedor == null ? null : new ProveedorDto
                {
                    ProveedorCifId = contacto.Proveedor.ProveedorCifId,
                    Nombre = contacto.Proveedor.Nombre,
                    DomicilioSocial = contacto.Proveedor.DomicilioSocial
                }
            }).ToList();
        }

        public async Task<ApiResponse<List<ContactoDto>>> ObtenerActivosPorProveedorAsync ()
        {
            try
            {
                var contactos = await _contactRepository.ObtenerPorEstadoConProveedorAsync(true);
                var listaContactos = MappearAContactoDto(contactos);

                return ApiResponse<List<ContactoDto>>.SuccessResponse(listaContactos, "Contactos activos obtenidos correctamente");

            }
            catch (Exception ex)
            {
                return ApiResponse<List<ContactoDto>>.ErrorResponse("Error al obtener la lista de contactos.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<List<ContactoDto>>> ObtenerEliminadosPorProveedorAsync ()
        {
            try
            {
                var contactos = await _contactRepository.ObtenerPorEstadoConProveedorAsync(false);
                var listaContactos = MappearAContactoDto(contactos);

                return ApiResponse<List<ContactoDto>>.SuccessResponse(listaContactos, "Contactos Eliminados obtenidos correctamente");

            }
            catch (Exception ex)
            {
                return ApiResponse<List<ContactoDto>>.ErrorResponse("Error al obtener la lista de contactos eliminados.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<ContactoDto>> ObtenerPorIdAsync ( int id )
        {
            try
            {
                var contacto = await _contactRepository.ObtenerPorIdAsync(id);
                if (contacto == null)
                    return ApiResponse<ContactoDto>.ErrorResponse("Contacto no encontrado.", $"No se encontro contacto con ID {id}.", 404);

                var resultado = new ContactoDto 
                {
                    ContactoId = contacto.ContactoId,
                    Nombre = contacto.Nombre,
                    Descripcion = contacto.Descripcion,
                    Correo = contacto.Correo,
                    Telefono = contacto.Telefono,
                    Estado = contacto.Estado
                };

                return ApiResponse<ContactoDto>.SuccessResponse(resultado, "Operacion exitosa");
            }
            catch (Exception ex)
            {
                return ApiResponse<ContactoDto>.ErrorResponse("Error al obtener contacto.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<List<ContactoDto>>> ObtenerPorNombreAsync ( string nombre )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    return ApiResponse<List<ContactoDto>>.ErrorResponse("El nombre no puede estar vacío.", 
                        "El parámetro nombre es requerido.", 400);

                var contacto = await _contactRepository.ObtenerPorNombreAsync(nombre);
                if (contacto == null)
                    return ApiResponse<List<ContactoDto>>.ErrorResponse("Contacto no encontrado.", 
                        $"No se encontro contacto con ID {nombre}.", 404);

                var resultado = MappearAContactoDto(contacto);
                
                return ApiResponse<List<ContactoDto>>.SuccessResponse(resultado, "Operacion exitosa");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ContactoDto>>.ErrorResponse("Error al obtener contacto.",ex.Message, 500);
            }
        }



        public async Task<ApiResponse<bool>> AgregarAsync ( ContactoDto contacto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(contacto);
                ArgumentNullException.ThrowIfNull(contacto.ProveedorCif);

                var proveedorExiste = await _supplierRepository.ObtenerPorIdAsync(contacto.ProveedorCif);
                if (proveedorExiste == null)
                    return ApiResponse<bool>.ErrorResponse("Proveedor no valido.", 
                        $"No se encontró ningun proveedor con CIF {contacto.ProveedorCif}.", 400);

                var nuevoContacto = new Contacto
                {
                    Nombre = contacto.Nombre,
                    Correo = contacto.Correo,
                    Telefono = contacto.Telefono,
                    Descripcion = contacto.Descripcion,
                    ProveedorCifId = contacto.ProveedorCif!
                };

                var resultado = await _contactRepository.AgregarAsync(nuevoContacto);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Contacto agregado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo agregar al contacto.", "Fallo en el repositorio.", 500);
            }
            catch (ArgumentNullException ex)
            {
                return ApiResponse<bool>.ErrorResponse(
                    "Faltan datos obligatorios para agregar el contacto.",
                    ex.Message, 400);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al agregar el contacto.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ActualizarAsync ( ContactoDto contacto )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(contacto);
                ArgumentNullException.ThrowIfNull(contacto.ProveedorCif);
                if (contacto.ContactoId == null)
                    return ApiResponse<bool>.ErrorResponse("ID del contacto requerido.", "Falta el ID para actualizar.", 400);

                var contactoActualizado = new Contacto
                {
                    ContactoId = contacto.ContactoId.Value,
                    Nombre = contacto.Nombre,
                    Descripcion = contacto.Descripcion,
                    Correo = contacto.Correo,
                    Telefono = contacto.Telefono,
                    ProveedorCifId = contacto.ProveedorCif
                };
                var resultado = await _contactRepository.ActualizarAsync( contactoActualizado );
                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Contacto actualizado correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo actualizar el contacto.", "Fallo en el repositorio.", 500);
            }
            catch (ArgumentNullException ex)
            {
                return ApiResponse<bool>.ErrorResponse(
                    "Faltan datos obligatorios para actualizar el contacto.",
                    ex.Message, 400);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al actualizar el contacto.", ex.Message, 500);
            }
        }


        public async Task<ApiResponse<bool>> EliminarAsync ( int id )
        {
            try
            {
                var resultado = await _contactRepository.CambiarEstadoAsync(id, false); 
                
                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Contacto se eliminó correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo eliminar el contacto.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al eliminar contacto.", ex.Message, 500);
            }
        }

        public async Task<ApiResponse<bool>> ReactivarAsync ( int id )
        {
            try
            {
                var resultado = await _contactRepository.CambiarEstadoAsync(id, true);

                return resultado
                    ? ApiResponse<bool>.SuccessResponse(true, "Contacto se reactivo correctamente.")
                    : ApiResponse<bool>.ErrorResponse("No se pudo reactivar el contacto.", "Fallo en el repositorio.", 500);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Error al reactivar contacto.", ex.Message, 500);
            }
        }

    }
}
