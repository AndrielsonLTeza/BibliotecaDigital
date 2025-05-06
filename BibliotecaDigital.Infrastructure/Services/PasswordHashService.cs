using BibliotecaDigital.Core.Interfaces;

namespace BibliotecaDigital.Infrastructure.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        private const int WorkFactor = 12; // Ajuste conforme necessário
        
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }
        
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}

// BibliotecaDigital.Core/Interfaces/IPasswordHashService.cs
namespace BibliotecaDigital.Core.Interfaces
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}