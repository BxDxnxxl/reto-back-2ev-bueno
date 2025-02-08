using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public interface IVideojuegoRepository
    {
        Task<List<Videojuego>> GetAllAsync();
        Task<Videojuego?> GetByIdAsync(int id);
        Task AddAsync(Videojuego videojuego);
        Task UpdateAsync(Videojuego videojuego);
        Task DeleteAsync(int id);
    }
}
