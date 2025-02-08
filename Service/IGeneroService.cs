using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public interface IGeneroService
    {
        Task<List<Genero>> GetAllAsync();
        Task<Genero?> GetByIdAsync(int id);
        Task AddAsync(Genero genero);
        Task UpdateAsync(Genero genero);
        Task DeleteAsync(int id);
    }
}
