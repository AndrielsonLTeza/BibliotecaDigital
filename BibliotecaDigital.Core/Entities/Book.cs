using System;
using System.Collections.Generic;
using BibliotecaDigital.Core.Interfaces;

namespace BibliotecaDigital.Core.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } 
        
        public int PublishedYear { get; set; } // Renomeado de PublicationYear
        
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int PageCount { get; set; }
        public string CoverImageUrl { get; set; }

        public int GenreId { get; set; }           // Nova propriedade
        public Genre Genre { get; set; }           // Nova propriedade de navegação

        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}
