using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qinshift.MovieRent.Services.Interface;
using Qinshift.MovieRent.Shared.Exceptions;
using Serilog;
using System.Diagnostics;

namespace Qinshift.MovieRent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet("all")]
        public IActionResult Get()
        {
            try
            {
                //Log.Debug("Entered get all movies.");
                Stopwatch stopwatch = Stopwatch.StartNew();

                Log.Debug("Fetch all movies started.");
                var movies = _movieService.GetAllMovies();

                stopwatch.Stop();
                Log.Debug($"Fetch movies finished in:{stopwatch.ElapsedMilliseconds}");

                if(movies != null && movies.Any())
                {
                    return Ok(movies);
                }
                throw new MovieDataException("There are not movies yet in the database.");
            }
            catch (MovieDataException ex)
            {
                Log.Error(ex, "Error occured while fetching all movies.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error occured while fetching all movies.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            finally
            {
                //Log.Debug("Finished get all movies.");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id must have positive value!");
                }
                var movie = _movieService.GetMovieById(id);
                if (movie == null)
                    throw new MovieNotFoundException($"Movie with Id:{id} not found.");
                return Ok(movie);
            }
            catch (MovieNotFoundException ex)
            {
                Log.Error($"Error occured while fetching movie with Id: {id}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                
                Log.Error($"Error occured while fetching movie with Id: {id}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("queryString")]
        public IActionResult GetByIdQueryString([FromQuery] int id)
        {
            try
            {
                var queryString = HttpContext.Request.QueryString;
                if(queryString.Value.Contains('='))
                    return Ok(_movieService.GetMovieById(id));
                throw new QueryStringNotInCorrectFormatException("Query string not in correct format.");
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured while fetching movie as query string with Id: {id}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("filterMovies")]
        public IActionResult FilterMovies(string genre, int year)
        {
            try
            {
                if (string.IsNullOrEmpty(genre) && year <= 0)
                {
                    return BadRequest("Genre or Year must be provided!");
                }
                return Ok(_movieService.FilterMovies(genre, year));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured while filtering movies by Genre: {genre} and year: {year}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
