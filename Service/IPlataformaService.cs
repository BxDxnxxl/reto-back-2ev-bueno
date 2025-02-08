using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public interface IPlataformaService
    {
        Task<List<Plataforma>> GetAllAsync();
        Task<Plataforma?> GetByIdAsync(int id);
        Task AddAsync(Plataforma plataforma);
        Task UpdateAsync(Plataforma plataforma);
        Task DeleteAsync(int id);
    }
}
