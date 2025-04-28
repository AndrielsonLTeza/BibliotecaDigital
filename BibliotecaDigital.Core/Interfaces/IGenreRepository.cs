using BibliotecaDigital.Core.Entities;

namespace BibliotecaDigital.Core.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre?> GetByIdAsync(int id);
        Task<Genre> AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(int id);
        Task<IEnumerable<Book>> GetBooksByGenreIdAsync(int genreId);
    }
}