using Models;
using Videojuegos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public class CompaniaService : ICompaniaService
    {
        private readonly ICompaniaRepository _companiaRepository;

        public CompaniaService(ICompaniaRepository companiaRepository)
        {
            _companiaRepository = companiaRepository;
        }

        public async Task<List<Compania>> GetAllAsync()
        {
            return await _companiaRepository.GetAllAsync();
        }

        public async Task<Compania?> GetByIdAsync(int id)
        {
            return await _companiaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Compania compania)
        {
            await _companiaRepository.AddAsync(compania);
        }

        public async Task UpdateAsync(Compania compania)
        {
            await _companiaRepository.UpdateAsync(compania);
        }

        public async Task DeleteAsync(int id)
        {
            await _companiaRepository.DeleteAsync(id);
        }
    }
}
