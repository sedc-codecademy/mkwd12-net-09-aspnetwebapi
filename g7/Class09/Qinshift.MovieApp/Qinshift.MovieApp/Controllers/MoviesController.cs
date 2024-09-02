using Microsoft.AspNetCore.Mvc;
using Qinshift.MovieApp.Domain.Enums;
using Qinshift.MovieApp.Dtos.MovieDtos;
using Qinshift.MovieApp.Services.Interfaces;
using Qinshift.MovieApp.Shared.CustomExceptions.MovieExceptions;

namespace Qinshift.MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet] 
        public ActionResult<List<MovieDto>> Get()
        {
            try
            {
                return Ok(_movieService.GetAllMovies());
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin");
            }
        }

        [HttpGet("filter")]
        public ActionResult<List<MovieDto>> Filter(int year, GenreEnum? genre)
        {
            try
            {
                return Ok(_movieService.FilterMovies(year, genre));
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin");
            }
        }

        [HttpGet("{id}")] 
        public ActionResult<MovieDto> GetById(int id)
        {
            try
            {
                return Ok(_movieService.GetMovieById(id));
            }
            catch (MovieNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin");
            }
        }

        [HttpPut]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto movie)
        {
            try
            {
                _movieService.UpdateMovie(movie);
                return StatusCode(StatusCodes.Status204NoContent, "Note updated!");
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _movieService.DeleteMovie(id);

                return StatusCode(StatusCodes.Status204NoContent, "Deleted resource");
            }
            catch (MovieNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin");
            }
        }

        [HttpPost("addMovie")]
        public IActionResult AddMovie([FromBody] AddMovieDto movieDto)
        {
            try
            {
                _movieService.AddMovie(movieDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin");
            }
        }
    }
}
