using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public interface IGeneroRepository
    {
        Task<List<Genero>> GetAllAsync();
        Task<Genero?> GetByIdAsync(int id);
        Task AddAsync(Genero genero);
        Task UpdateAsync(Genero genero);
        Task DeleteAsync(int id);
        Task<List<Genero>> GetTop5GenresAsync();
    }
}
