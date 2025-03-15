using Models;
using Videojuegos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioService(IComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }

        public async Task<List<Comentario>> GetAllAsync()
        {
            return await _comentarioRepository.GetAllAsync();
        }

        public async Task<Comentario?> GetByIdAsync(int id)
        {
            return await _comentarioRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Comentario comentario)
        {
            await _comentarioRepository.AddAsync(comentario);
        }
        
        public async Task UpdateAsync(Comentario comentario)
        {
            await _comentarioRepository.UpdateAsync(comentario);
        }


        public async Task DeleteAsync(int id)
        {
            await _comentarioRepository.DeleteAsync(id);
        }

        public async Task<List<ComentarioDto>> GetComentariosByVideojuegoAsync(int videojuegoId)
        {
            return await _comentarioRepository.GetComentariosByVideojuegoIdAsync(videojuegoId);
        }

        public async Task<bool> UpdateLikesDislikesAsync(int comentarioId, bool isLike)
        {
            return await _comentarioRepository.UpdateLikesDislikesAsync(comentarioId, isLike);
        }
    }
}
