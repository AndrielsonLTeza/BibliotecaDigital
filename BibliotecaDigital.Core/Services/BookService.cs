using BibliotecaDigital.Core.Entities;
using BibliotecaDigital.Core.Interfaces;

namespace BibliotecaDigital.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;

        public BookService(IBookRepository bookRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreIdAsync(int genreId)
        {
            return await _bookRepository.GetByGenreIdAsync(genreId);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            ValidateBook(book);

            // Verify genre exists
            var genre = await _genreRepository.GetByIdAsync(book.GenreId);
            if (genre == null)
                throw new KeyNotFoundException($"Genre with ID {book.GenreId} not found");

            return await _bookRepository.AddAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            ValidateBook(book);

            var existingBook = await _bookRepository.GetByIdAsync(book.Id);
            if (existingBook == null)
                throw new KeyNotFoundException($"Book with ID {book.Id} not found");

            // Verify genre exists
            var genre = await _genreRepository.GetByIdAsync(book.GenreId);
            if (genre == null)
                throw new KeyNotFoundException($"Genre with ID {book.GenreId} not found");

            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");

            await _bookRepository.DeleteAsync(id);
        }

        private void ValidateBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Book title is required");

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new ArgumentException("Book author is required");

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new ArgumentException("Book ISBN is required");
            
            // Basic ISBN validation (simplified)
            if (!IsValidISBN(book.ISBN))
                throw new ArgumentException("Invalid ISBN format");

            if (book.PublishedYear <= 0)
                throw new ArgumentException("Published year must be positive");
        }

        private bool IsValidISBN(string isbn)
        {
            // Simplified ISBN validation (just length check)
            // Real implementation would do proper ISBN-10 or ISBN-13 validation
            return !string.IsNullOrWhiteSpace(isbn) && 
                   (isbn.Replace("-", "").Length == 10 || isbn.Replace("-", "").Length == 13);
        }
    }
}