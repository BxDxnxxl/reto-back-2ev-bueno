using Models;
using Videojuegos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public class PlataformaService : IPlataformaService
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public PlataformaService(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;
        }

        public async Task<List<Plataforma>> GetAllAsync()
        {
            return await _plataformaRepository.GetAllAsync();
        }

        public async Task<Plataforma?> GetByIdAsync(int id)
        {
            return await _plataformaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Plataforma plataforma)
        {
            await _plataformaRepository.AddAsync(plataforma);
        }

        public async Task UpdateAsync(Plataforma plataforma)
        {
            await _plataformaRepository.UpdateAsync(plataforma);
        }

        public async Task DeleteAsync(int id)
        {
            await _plataformaRepository.DeleteAsync(id);
        }
    }
}
