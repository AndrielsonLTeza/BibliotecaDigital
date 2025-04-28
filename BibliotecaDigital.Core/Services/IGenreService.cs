using BibliotecaDigital.Core.Entities;

namespace BibliotecaDigital.Core.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre?> GetGenreByIdAsync(int id);
        Task<Genre> CreateGenreAsync(Genre genre);
        Task UpdateGenreAsync(Genre genre);
        Task DeleteGenreAsync(int id);
        Task<IEnumerable<Book>> GetBooksByGenreIdAsync(int genreId);
    }
}
