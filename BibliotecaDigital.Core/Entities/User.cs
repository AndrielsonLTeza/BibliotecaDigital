
using System;
using System.Collections.Generic;

namespace BibliotecaDigital.Core.Entities
{
    public class User
    {
       
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    // Propriedade de navegação para empréstimos
    public List<string> Roles { get; set; } = new List<string>();

    }
}