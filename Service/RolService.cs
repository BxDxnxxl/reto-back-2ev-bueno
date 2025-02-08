using Models;
using Videojuegos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<List<Rol>> GetAllAsync()
        {
            return await _rolRepository.GetAllAsync();
        }

        public async Task<Rol?> GetByIdAsync(int id)
        {
            return await _rolRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Rol rol)
        {
            await _rolRepository.AddAsync(rol);
        }

        public async Task UpdateAsync(Rol rol)
        {
            await _rolRepository.UpdateAsync(rol);
        }

        public async Task DeleteAsync(int id)
        {
            await _rolRepository.DeleteAsync(id);
        }
    }
}
