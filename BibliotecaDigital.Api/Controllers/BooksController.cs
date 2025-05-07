using BibliotecaDigital.Core.DTOs;
using BibliotecaDigital.Core.Entities;
using BibliotecaDigital.Core.Interfaces;
using BibliotecaDigital.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BibliotecaDigital.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();

            var response = books.Select(b => new BookResponseDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                Description = b.Description,
                PublicationYear = b.PublishedYear,
                Publisher = b.Publisher,
                Language = b.Language,
                PageCount = b.PageCount,
                CoverImageUrl = b.CoverImageUrl,
                Category = b.Category,
                IsAvailable = b.IsAvailable,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookResponseDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound($"Livro com ID {id} não encontrado");

            var response = new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Description = book.Description,
                PublicationYear = book.PublishedYear,
                Publisher = book.Publisher,
                Language = book.Language,
                PageCount = book.PageCount,
                CoverImageUrl = book.CoverImageUrl,
                Category = book.Category,
                IsAvailable = book.IsAvailable,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt
            };

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> CreateBook([FromBody] BookCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = new Book
            {
                Title = model.Title,
                Author = model.Author,
                ISBN = model.ISBN,
                Description = model.Description,
                PublishedYear = model.PublicationYear,
                Publisher = model.Publisher,
                Language = model.Language,
                PageCount = model.PageCount,
                CoverImageUrl = model.CoverImageUrl,
                Category = model.Category,
                IsAvailable = true,
                CreatedAt = DateTime.Now
            };

            var bookId = await _bookService.CreateBookAsync(book);

            return CreatedAtAction(nameof(GetBookById), new { id = bookId },
                $"Livro '{model.Title}' criado com sucesso");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] BookUpdateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null)
                return NotFound($"Livro com ID {id} não encontrado");

            if (!string.IsNullOrWhiteSpace(model.Title))
                existingBook.Title = model.Title;

            if (!string.IsNullOrWhiteSpace(model.Author))
                existingBook.Author = model.Author;

            if (!string.IsNullOrWhiteSpace(model.ISBN))
                existingBook.ISBN = model.ISBN;

            if (!string.IsNullOrWhiteSpace(model.Description))
                existingBook.Description = model.Description;

            if (model.PublicationYear.HasValue)
                existingBook.PublishedYear = model.PublicationYear.Value;

            if (!string.IsNullOrWhiteSpace(model.Publisher))
                existingBook.Publisher = model.Publisher;

            if (!string.IsNullOrWhiteSpace(model.Language))
                existingBook.Language = model.Language;

            if (model.PageCount.HasValue)
                existingBook.PageCount = model.PageCount.Value;

            if (!string.IsNullOrWhiteSpace(model.CoverImageUrl))
                existingBook.CoverImageUrl = model.CoverImageUrl;

            if (!string.IsNullOrWhiteSpace(model.Category))
                existingBook.Category = model.Category;

            existingBook.UpdatedAt = DateTime.Now;

            var success = await _bookService.UpdateBookAsync(existingBook);
            if (!success)
                return StatusCode(500, "Erro ao atualizar o livro");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound($"Livro com ID {id} não encontrado");

            var success = await _bookService.DeleteBookAsync(id);
            if (!success)
                return StatusCode(500, "Erro ao excluir o livro");

            return NoContent();
        }

        [HttpPost("{id}/borrow")]
        [Authorize]
        public async Task<ActionResult> BorrowBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound($"Livro com ID {id} não encontrado");

            if (!book.IsAvailable)
                return BadRequest("Este livro não está disponível para empréstimo");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("Usuário não identificado");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("ID de usuário inválido");

            var success = await _bookService.BorrowBookAsync(id, userId);
            if (!success)
                return StatusCode(500, "Erro ao processar o empréstimo");

            return Ok($"Livro '{book.Title}' emprestado com sucesso");
        }

        [HttpPost("{id}/return")]
        [Authorize]
        public async Task<ActionResult> ReturnBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound($"Livro com ID {id} não encontrado");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("Usuário não identificado");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("ID de usuário inválido");

            var success = await _bookService.ReturnBookAsync(id, userId);
            if (!success)
                return BadRequest("Você não tem um empréstimo ativo para este livro");

            return Ok($"Livro '{book.Title}' devolvido com sucesso");
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BookResponseDto>>> SearchBooks([FromQuery] string term)
        {
            var books = await _bookService.SearchBooksAsync(term);

            var response = books.Select(b => new BookResponseDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                Description = b.Description,
                PublicationYear = b.PublishedYear,
                Publisher = b.Publisher,
                Language = b.Language,
                PageCount = b.PageCount,
                CoverImageUrl = b.CoverImageUrl,
                Category = b.Category,
                IsAvailable = b.IsAvailable,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            });

            return Ok(response);
        }

        [HttpGet("category/{category}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetBooksByCategory(string category)
        {
            var books = await _bookService.GetBooksByCategory(category);

            var response = books.Select(b => new BookResponseDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                Description = b.Description,
                PublicationYear = b.PublishedYear,
                Publisher = b.Publisher,
                Language = b.Language,
                PageCount = b.PageCount,
                CoverImageUrl = b.CoverImageUrl,
                Category = b.Category,
                IsAvailable = b.IsAvailable,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            });

            return Ok(response);
        }
    }
}
