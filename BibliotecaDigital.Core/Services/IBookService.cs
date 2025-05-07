using BibliotecaDigital.Core.Entities;

namespace BibliotecaDigital.Core.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> GetBooksByGenreIdAsync(int genreId);
        Task<Book> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
        Task<bool> BorrowBookAsync(int bookId, int userId);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
        Task<bool> ReturnBookAsync(int bookId, int userId);
        Task<IEnumerable<Book>> GetBooksByCategory(string category);

    }
}
