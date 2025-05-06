using BibliotecaDigital.Core.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BibliotecaDigital.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByUserName(string userName);
        Task<User> GetUserByEmail(string email);
        Task<bool> UserExistsByEmail(string email);
        Task<int> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);

        // Novo método sugerido:
        Task<User> GetUserFromClaims(ClaimsPrincipal user);
    }
}
