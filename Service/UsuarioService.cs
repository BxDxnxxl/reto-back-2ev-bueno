using Models;
using Videojuegos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<List<Usuario>> GetUsuariosByNombreAsync(string nombre)
        {
            return await _usuarioRepository.GetUsuariosByNombreAsync(nombre);
        }

        public async Task<LoginDto?> LoginAsync(string username, string password)
        {
            return await _usuarioRepository.LoginAsync(username, password);
        }

        public async Task CreacionBasicaAsync(UsuarioCreacionBaseDto usuario)
        {
            await _usuarioRepository.CreacionBasicaAsync(usuario);
        }
    }
}
