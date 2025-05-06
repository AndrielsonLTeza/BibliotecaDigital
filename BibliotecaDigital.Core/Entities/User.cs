
using System;
using System.Collections.Generic;

namespace BibliotecaDigital.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; } = true;
        public string Role { get; set; } = "Usuario";
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        
        // Propriedade de navegação para empréstimos
        public ICollection<Loan> Emprestimos { get; set; }
    }
}