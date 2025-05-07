using BibliotecaDigital.Core.Entities;
using BibliotecaDigital.Core.Interfaces;
using BibliotecaDigital.Core.Services;
using BibliotecaDigital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaDigital.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<int> CreateBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            book.UpdatedAt = DateTime.Now;
            _context.Books.Update(book);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> BorrowBookAsync(int bookId, int userId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || !book.IsAvailable)
                return false;

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            // Criar empréstimo
            var loan = new Loan
            {
                BookId = bookId,
                UserId = userId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14), // 2 semanas para devolução
                IsReturned = false,
                Status = "Ativo"
            };

            // Atualizar disponibilidade do livro
            book.IsAvailable = false;

            _context.Add(loan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReturnBookAsync(int bookId, int userId)
        {
            // Encontrar o empréstimo ativo
            var loan = await _context.Set<Loan>()
                .Where(l => l.BookId == bookId && l.UserId == userId && !l.IsReturned)
                .FirstOrDefaultAsync();

            if (loan == null)
                return false;

            // Atualizar empréstimo
            loan.ReturnDate = DateTime.Now;
            loan.IsReturned = true;
            loan.Status = "Devolvido";

            // Atualizar disponibilidade do livro
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                book.IsAvailable = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllBooksAsync();

            return await _context.Books
                .AsNoTracking()
                .Where(b => b.Title.Contains(searchTerm) || 
                           b.Author.Contains(searchTerm) || 
                           b.ISBN.Contains(searchTerm) ||
                           b.Publisher.Contains(searchTerm) ||
                           b.Description.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return await GetAllBooksAsync();

            return await _context.Books
                .AsNoTracking()
                .Where(b => b.Category == category)
                .ToListAsync();
        }

        public Task<IEnumerable<Book>> GetBooksByGenreIdAsync(int genreId)
        {
            throw new NotImplementedException();
        }

        Task<Book> IBookService.CreateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBookService.UpdateBookAsync(Book book)
        {
            return UpdateBookAsync(book);
        }

        Task<bool> IBookService.DeleteBookAsync(int id)
        {
            return DeleteBookAsync(id);
        }
    }
}