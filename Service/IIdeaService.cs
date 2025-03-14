using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public interface IIdeaService
    {
        Task<List<Ideas>> GetAllAsync();
        Task AddAsync(Ideas idea);
        Task DeleteAsync(int id);
        Task<List<IdeaDto>> GetAllIdeasConNombreUsuarioAsync();
    }
}
