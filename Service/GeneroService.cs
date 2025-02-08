using Models;
using Videojuegos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<List<Genero>> GetAllAsync()
        {
            return await _generoRepository.GetAllAsync();
        }

        public async Task<Genero?> GetByIdAsync(int id)
        {
            return await _generoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Genero genero)
        {
            await _generoRepository.AddAsync(genero);
        }

        public async Task UpdateAsync(Genero genero)
        {
            await _generoRepository.UpdateAsync(genero);
        }

        public async Task DeleteAsync(int id)
        {
            await _generoRepository.DeleteAsync(id);
        }
    }
}
