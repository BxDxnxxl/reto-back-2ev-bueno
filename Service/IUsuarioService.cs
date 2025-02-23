using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Service
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<List<Usuario>> GetUsuariosByNombreAsync(string nombre);
        Task<UserInfoRoles?> LoginAsync(LoginRequestDto loginUsuario);
        Task <int>CreacionBasicaAsync(UsuarioCreacionBaseDto usuario);
        Task<List<UserInfoRoles>> GetUsuariosConRolesAsync();
        Task<UserInfoRoles?> GetUsuarioConRolesByIdAsync(int usuarioId);
    }
}
