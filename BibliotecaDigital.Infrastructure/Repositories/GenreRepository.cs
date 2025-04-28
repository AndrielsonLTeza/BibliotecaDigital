using BibliotecaDigital.Core.Entities;
using BibliotecaDigital.Core.Interfaces;
using BibliotecaDigital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreIdAsync(int genreId)
        {
            return await _context.Books
                .Where(b => b.GenreId == genreId)
                .ToListAsync();
        }
    }
}
