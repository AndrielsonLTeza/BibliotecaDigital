using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaDigital.Core.DTOs
{
    public class BookCreateDto
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório")]
        [StringLength(100, ErrorMessage = "O autor deve ter no máximo 100 caracteres")]
        public string Author { get; set; }

        [StringLength(20, ErrorMessage = "O ISBN deve ter no máximo 20 caracteres")]
        public string ISBN { get; set; }

        [StringLength(2000, ErrorMessage = "A descrição deve ter no máximo 2000 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        [Range(1000, 3000, ErrorMessage = "O ano de publicação deve estar entre 1000 e 3000")]
        public int PublicationYear { get; set; }

        [StringLength(100, ErrorMessage = "A editora deve ter no máximo 100 caracteres")]
        public string Publisher { get; set; }

        [StringLength(50, ErrorMessage = "O idioma deve ter no máximo 50 caracteres")]
        public string Language { get; set; }

        [Range(1, 10000, ErrorMessage = "O número de páginas deve estar entre 1 e 10000")]
        public int PageCount { get; set; }

        [Url(ErrorMessage = "A URL da capa deve ser uma URL válida")]
        public string CoverImageUrl { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória")]
        [StringLength(50, ErrorMessage = "A categoria deve ter no máximo 50 caracteres")]
        public string Category { get; set; }
    }

    public class BookUpdateDto
    {
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
        public string Title { get; set; }

        [StringLength(100, ErrorMessage = "O autor deve ter no máximo 100 caracteres")]
        public string Author { get; set; }

        [StringLength(20, ErrorMessage = "O ISBN deve ter no máximo 20 caracteres")]
        public string ISBN { get; set; }

        [StringLength(2000, ErrorMessage = "A descrição deve ter no máximo 2000 caracteres")]
        public string Description { get; set; }

        [Range(1000, 3000, ErrorMessage = "O ano de publicação deve estar entre 1000 e 3000")]
        public int? PublicationYear { get; set; }

        [StringLength(100, ErrorMessage = "A editora deve ter no máximo 100 caracteres")]
        public string Publisher { get; set; }

        [StringLength(50, ErrorMessage = "O idioma deve ter no máximo 50 caracteres")]
        public string Language { get; set; }

        [Range(1, 10000, ErrorMessage = "O número de páginas deve estar entre 1 e 10000")]
        public int? PageCount { get; set; }

        [Url(ErrorMessage = "A URL da capa deve ser uma URL válida")]
        public string CoverImageUrl { get; set; }

        [StringLength(50, ErrorMessage = "A categoria deve ter no máximo 50 caracteres")]
        public string Category { get; set; }
    }

    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public int PublicationYear { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int PageCount { get; set; }
        public string CoverImageUrl { get; set; }
        public string Category { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class BookBorrowDto
    {
        [Required]
        public int BookId { get; set; }
        
        [Required]
        public int UserId { get; set; }
    }
}