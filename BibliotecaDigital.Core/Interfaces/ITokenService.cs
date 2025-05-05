
using BibliotecaDigital.Core.Entities;

namespace BibliotecaDigital.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
