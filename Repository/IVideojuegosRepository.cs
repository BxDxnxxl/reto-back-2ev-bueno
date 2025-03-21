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
        Task<VideojuegoDetalleDto?> GetDetalleByIdAsync(int id);
        Task<List<Videojuego>> FiltrarVideojuegosAsync(string? compania, string? genero, string? plataforma);
        Task<List<Videojuego>> BuscarVideojuegosAsync(string filtro);
        Task<List<Videojuego>> GetTop5MejoresVideojuegosValoradosAsync();
    }
}
