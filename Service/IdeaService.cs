using Models;
using Videojuegos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public class IdeaService : IIdeaService
    {
        private readonly IIdeaRepository _ideaRepository;

        public IdeaService(IIdeaRepository ideaRepository)
        {
            _ideaRepository = ideaRepository;
        }

        public async Task<List<Ideas>> GetAllAsync()
        {
            return await _ideaRepository.GetAllAsync();
        }

        public async Task AddAsync(Ideas idea)
        {
            await _ideaRepository.AddAsync(idea);
        }

        public async Task DeleteAsync(int id)
        {
            await _ideaRepository.DeleteAsync(id);
        }

        public async Task<List<IdeaDto>> GetAllIdeasConNombreUsuarioAsync()
        {
            return await _ideaRepository.GetAllIdeasConNombreUsuarioAsync();
        }
    }
}
