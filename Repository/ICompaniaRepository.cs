using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public interface ICompaniaRepository
    {
        Task<List<Compania>> GetAllAsync();
        Task<Compania?> GetByIdAsync(int id);
        Task AddAsync(Compania compania);
        Task UpdateAsync(Compania compania);
        Task DeleteAsync(int id);
    }
}
