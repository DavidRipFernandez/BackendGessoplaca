using NSoft.Models;
using NSoft.Repositories;

namespace NSoft.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task ActualizarAsync(Material material)
        {
            await _materialRepository.AgregarAsync(material);
        }

        public async Task AgregarAsync(Material material)
        {
            await _materialRepository.AgregarAsync(material);
        }

        public async Task EliminarAsync(int id)
        {
            await _materialRepository.EliminarAsync(id);
        }

        public async Task<Material> ObtenerPorIdAsync(int id)
        {
            return await _materialRepository.ObtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<Material>> ObtenerTodosAsync()
        {
           return await _materialRepository.ObtenerTodosAsync();
        }
    }
}
