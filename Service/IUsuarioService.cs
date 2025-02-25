using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<int> AddAsync(Usuario usuario);
        Task UpdateAsync(int id,Usuario usuario);
        Task DeleteAsync(int id);
        Task<List<Usuario>> GetUsuariosByNombreAsync(string nombre);
        Task <int>CreacionBasicaAsync(UsuarioCreacionBaseDto usuario);
        Task<List<UserInfoRoles>> GetUsuariosConRolesAsync();
        Task<UserInfoRoles?> GetUsuarioConRolesByIdAsync(int usuarioId);
    }
}
