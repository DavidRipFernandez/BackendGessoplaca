using NSoft.DTOs;
using NSoft.Models;
using NSoft.Repositories.IRepositories;
using NSoft.Services.IServices;

namespace NSoft.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<bool> ActualizarAsync( MaterialDto material )
        {
            ArgumentNullException.ThrowIfNull( material );
            var materialModificado = new Material
            {
                Nombre = material.Nombre,
                CodigoMaterial = material.CodigoMaterial,
                SistemaMedicion = material.SistemaMedicion,
                CategoriaId = material.CategoriaId
            };

            return await _materialRepository.AgregarAsync(materialModificado);
        }

        public async Task<bool> AgregarAsync( MaterialDto material )
        {
            ArgumentNullException.ThrowIfNull(material);
            var nuevoMaterial = new Material
            {
                Nombre = material.Nombre,
                CodigoMaterial = material.CodigoMaterial,
                SistemaMedicion = material.SistemaMedicion,
                CategoriaId = material.CategoriaId
            };

            return await _materialRepository.AgregarAsync(nuevoMaterial);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _materialRepository.EliminarAsync(id);
        }

        public async Task<MaterialDto> ObtenerPorIdAsync(int id)
        {
            var material = await _materialRepository.ObtenerPorIdAsync(id);
            if (material == null)
                return null;

            return new MaterialDto { 
                MaterialId = material.MaterialId,
                Nombre = material.Nombre,
                CodigoMaterial = material.CodigoMaterial,
                Estado = material.Estado,
                SistemaMedicion = material.SistemaMedicion,
                CategoriaId=material.CategoriaId
            };
        }

        public async Task<IEnumerable<MaterialDto>> ObtenerTodosAsync()
        {
            var materiales = await _materialRepository.ObtenerTodosAsync();
            return materiales.Select(material => new MaterialDto
            {
                MaterialId = material.MaterialId,
                Nombre = material.Nombre,
                CodigoMaterial = material.CodigoMaterial,
                Estado = material.Estado,
                SistemaMedicion = material.SistemaMedicion,
                CategoriaId = material.CategoriaId
            }).ToList();
        }
    }
}
