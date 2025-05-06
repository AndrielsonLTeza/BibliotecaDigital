
using BibliotecaDigital.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibliotecaDigital.Core.Interfaces;

namespace BibliotecaDigital.Core.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<int> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
        Task<bool> BorrowBookAsync(int bookId, int userId);
        Task<bool> ReturnBookAsync(int bookId, int userId);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
        Task<IEnumerable<Book>> GetBooksByCategory(string category);
    }
}