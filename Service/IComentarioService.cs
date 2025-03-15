using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public interface IComentarioService
    {
        Task<List<Comentario>> GetAllAsync();
        Task<Comentario?> GetByIdAsync(int id);
        Task AddAsync(Comentario comentario);
        Task UpdateAsync(Comentario comentario);
        Task DeleteAsync(int id);
        Task<List<ComentarioDto>> GetComentariosByVideojuegoAsync(int videojuegoId);
        Task<bool> UpdateLikesDislikesAsync(int comentarioId, bool isLike);
    }
}
