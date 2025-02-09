using Models;
using Videojuegos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Videojuegos.DTOs;

namespace Videojuegos.Service
{
    public class VideojuegoService : IVideojuegoService
    {
        private readonly IVideojuegoRepository _videojuegoRepository;

        public VideojuegoService(IVideojuegoRepository videojuegoRepository)
        {
            _videojuegoRepository = videojuegoRepository;
        }

        public async Task<List<Videojuego>> GetAllAsync()
        {
            return await _videojuegoRepository.GetAllAsync();
        }

        public async Task<Videojuego?> GetByIdAsync(int id)
        {
            return await _videojuegoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Videojuego videojuego)
        {
            await _videojuegoRepository.AddAsync(videojuego);
        }

        public async Task UpdateAsync(Videojuego videojuego)
        {
            await _videojuegoRepository.UpdateAsync(videojuego);
        }

        public async Task DeleteAsync(int id)
        {
            await _videojuegoRepository.DeleteAsync(id);
        }

        public async Task<VideojuegoDetalleDto?> GetDetalleByIdAsync(int id)
        {
            return await _videojuegoRepository.GetDetalleByIdAsync(id);
        }
    }
}
