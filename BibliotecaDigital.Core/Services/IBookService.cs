using BibliotecaDigital.Core.Entities;

namespace BibliotecaDigital.Core.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> GetBooksByGenreIdAsync(int genreId);
        Task<Book> CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
