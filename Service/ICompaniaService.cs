using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public interface ICompaniaService
    {
        Task<List<Compania>> GetAllAsync();
        Task<Compania?> GetByIdAsync(int id);
        Task AddAsync(Compania compania);
        Task UpdateAsync(Compania compania);
        Task DeleteAsync(int id);
    }
}
