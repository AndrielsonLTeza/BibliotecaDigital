using BibliotecaDigital.Core.Entities;
using BibliotecaDigital.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaDigital.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenreById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> CreateGenre(Genre genre)
        {
            try
            {
                var createdGenre = await _genreService.CreateGenreAsync(genre);
                return CreatedAtAction(nameof(GetGenreById), new { id = createdGenre.Id }, createdGenre);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, Genre genre)
        {
            if (id != genre.Id)
                return BadRequest("ID in URL does not match ID in request body");

            try
            {
                await _genreService.UpdateGenreAsync(genre);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                await _genreService.DeleteGenreAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByGenre(int id)
        {
            try
            {
                var books = await _genreService.GetBooksByGenreIdAsync(id);
                return Ok(books);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}