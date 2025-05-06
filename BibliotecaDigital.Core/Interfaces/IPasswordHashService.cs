using BibliotecaDigital.Core.Interfaces;
namespace BibliotecaDigital.Core.Interfaces
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}