using System.Collections.Generic;

namespace BibliotecaDigital.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }  // Será armazenado como hash
        public List<string> Roles { get; set; } = new List<string>();
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        
    }
}