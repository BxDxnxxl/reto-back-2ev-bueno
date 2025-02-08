using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public interface IVideojuegoService
    {
        Task<List<Videojuego>> GetAllAsync();
        Task<Videojuego?> GetByIdAsync(int id);
        Task AddAsync(Videojuego videojuego);
        Task UpdateAsync(Videojuego videojuego);
        Task DeleteAsync(int id);
    }
}
