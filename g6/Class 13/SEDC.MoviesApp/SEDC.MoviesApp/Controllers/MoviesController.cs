using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Dtos;
using SEDC.MoviesApp.Services.Interfaces;
using SEDC.MoviesApp.Shared;
using System.Security.Claims;

namespace SEDC.MoviesApp.Controllers
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

        [Authorize]
        [HttpGet] //api/movies
        public ActionResult<List<MovieDto>> Get()
        {
            try
            {
                var userId = GetAuthorizedUserId();
                return Ok(_movieService.GetAllMovies(userId));
            }
            catch (MovieException e)
            {
                //log
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin");
            }
        }
        [Authorize]
        [HttpGet("filter")]
        public ActionResult<List<MovieDto>> Filter(int year, GenreEnum? genre)
        {
            try
            {
                return Ok(_movieService.FilterMovies(year, genre));
            }
            catch (MovieException e)
            {
                //log
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin");
            }
        }

        [HttpGet("{id}")] //api/movies/2
        public ActionResult<MovieDto> GetById(int id)
        {
            try
            {
                return Ok(_movieService.GetMovieById(id));
            }
            catch (MovieNotFoundException e)
            {
                //log
                return NotFound(e.Message);
            }
            catch (MovieException e)
            {
                //log
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                //log
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
                //log
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                //log
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
                //log
                return NotFound(e.Message);
            }
            catch (MovieException e)
            {
                //log
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin");
            }
        }

        [Authorize]
        [HttpPost("addMovie")]
        public IActionResult AddMovie([FromBody] AddMovieDto movieDto)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                _movieService.AddMovie(movieDto, userId);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (MovieException e)
            {
                //log
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin");
            }
        }

        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?
                .Value, out var userId))
            {
                string name = User.FindFirst(ClaimTypes.Name)?.Value;
                throw new UserException(userId, name,
                    "Name identifier claim does not exist!");
            }
            return userId;
        }
    }
}
