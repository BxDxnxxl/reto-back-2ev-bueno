using Models;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public interface ITokenRepository
    {
        Task<UserInfoRoles?> LoginAsync(LoginRequestDto login);
    }
}
