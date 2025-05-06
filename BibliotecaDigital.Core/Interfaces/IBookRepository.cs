using BibliotecaDigital.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaDigital.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId);
        Task<Book> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
    }
}
