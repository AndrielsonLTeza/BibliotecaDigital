using Microsoft.AspNetCore.Identity;

namespace BibliotecaDigital.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Você pode adicionar propriedades personalizadas aqui, se necessário
        public string? FullName { get; set; }
    }
}
