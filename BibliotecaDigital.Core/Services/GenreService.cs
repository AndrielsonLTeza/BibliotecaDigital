using BibliotecaDigital.Core.Entities;
using BibliotecaDigital.Core.Interfaces;

namespace BibliotecaDigital.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _genreRepository.GetAllAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            return await _genreRepository.GetByIdAsync(id);
        }

        public async Task<Genre> CreateGenreAsync(Genre genre)
        {
            if (string.IsNullOrWhiteSpace(genre.Name))
                throw new ArgumentException("Genre name is required");

            return await _genreRepository.AddAsync(genre);
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            if (string.IsNullOrWhiteSpace(genre.Name))
                throw new ArgumentException("Genre name is required");

            var existingGenre = await _genreRepository.GetByIdAsync(genre.Id);
            if (existingGenre == null)
                throw new KeyNotFoundException($"Genre with ID {genre.Id} not found");

            await _genreRepository.UpdateAsync(genre);
        }

        public async Task DeleteGenreAsync(int id)
        {
            var existingGenre = await _genreRepository.GetByIdAsync(id);
            if (existingGenre == null)
                throw new KeyNotFoundException($"Genre with ID {id} not found");

            await _genreRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreIdAsync(int genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);
            if (genre == null)
                throw new KeyNotFoundException($"Genre with ID {genreId} not found");

            return await _genreRepository.GetBooksByGenreIdAsync(genreId);
        }
    }
}
