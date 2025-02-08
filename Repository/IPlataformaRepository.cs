using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public interface IPlataformaRepository
    {
        Task<List<Plataforma>> GetAllAsync();
        Task<Plataforma?> GetByIdAsync(int id);
        Task AddAsync(Plataforma plataforma);
        Task UpdateAsync(Plataforma plataforma);
        Task DeleteAsync(int id);
    }
}
