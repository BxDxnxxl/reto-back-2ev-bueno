using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task <List<Usuario>> GetUsuariosByNombreAsync(string nombre);
        Task<LoginDto?> LoginAsync(string username, string password);

        Task CreacionBasicaAsync(UsuarioCreacionBaseDto usuario);
        Task<List<UserInfoRoles>> GetUsuariosConRolesAsync();
    }
}
