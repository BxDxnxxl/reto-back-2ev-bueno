using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public interface IIdeaRepository
    {
        Task<List<Ideas>> GetAllAsync();
        Task AddAsync(Ideas idea);
        Task DeleteAsync(int id);
        Task<List<IdeaDto>> GetAllIdeasConNombreUsuarioAsync();
    }
}
