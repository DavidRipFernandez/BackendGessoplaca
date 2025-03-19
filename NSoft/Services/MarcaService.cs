using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class MarcaService:IMarcaService
    {

        private readonly IMarcaRepository _marcaRepository;

        public MarcaService (IMarcaRepository marcaRepository )
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<bool> ActualizarAsync ( MarcaDto marca )
        {
            ArgumentNullException.ThrowIfNull ( marca );
            var marcaModificada = new Marca
            {
                MarcaId = marca.MarcaId,
                Nombre = marca.Nombre,
                Descripcion = marca.Descripcion
            };

            return await _marcaRepository.ActualizarAsync(marcaModificada);

        }

        public async Task<bool> AgregarAsync ( MarcaDto marca )
        {
            ArgumentNullException.ThrowIfNull( marca );
            var nuevaMarca = new Marca { 
                Nombre = marca.Nombre, 
                Descripcion= marca.Descripcion
            };

            return await _marcaRepository.AgregarAsync(nuevaMarca);

        }

        public async Task<bool> EliminarAsync ( int id )
        {
            return await _marcaRepository.EliminarAsync(id);
        }

        public async Task<IEnumerable<MarcaDto>> ObtenerActivosAsync ()
        {
            var marcasActivas = await _marcaRepository.ObtenerActivosAsync();
            if ( marcasActivas != null ) 
                return null;

            return marcasActivas.Select(marca => new MarcaDto
            {
                MarcaId = marca.MarcaId,
                Nombre= marca.Nombre,
                Descripcion = marca.Descripcion
            }).ToList();
        }

        public async Task<IEnumerable<MarcaDto>> ObtenerEliminadosAsync ()
        {
            var marcasEliminadas = await _marcaRepository.ObtenerEliminadosAsync();
            if (marcasEliminadas != null)
                return null;

            return marcasEliminadas.Select(marca => new MarcaDto
            {
                MarcaId= marca.MarcaId,
                Nombre = marca.Nombre,
                Descripcion= marca.Descripcion
            }).ToList ();
        }

        public async Task<MarcaDto> ObtenerPorIdAsync ( int id )
        {
            var marca = await _marcaRepository.ObtenerPorIdAsync( id );
            if (marca == null)
                return null;

            return new MarcaDto
            {
                MarcaId = marca.MarcaId,
                Nombre = marca.Nombre,
                Descripcion = marca?.Descripcion
            };

        }

        public async Task<MarcaDto?> ObtenerPorIdConProveedoresAsync ( int id )
        {
            var marca = await _marcaRepository.ObtenerPorIdConProveedoresAsync(id);
            if (marca == null)
                return null;

            return new MarcaDto
            {
                MarcaId = marca.MarcaId,
                Nombre = marca.Nombre,
                Descripcion = marca?.Descripcion,
                ProveedoresMarcas = marca.ProveedoresMarcas.Select(p => new ProveedorDto
                {
                    ProveedorCifId = p.Proveedor.ProveedorCifId,
                    Nombre = p.Proveedor.Nombre,
                    DomicilioSocial = p.Proveedor.DomicilioSocial
                }).ToList()
            };
        }
    }
}
