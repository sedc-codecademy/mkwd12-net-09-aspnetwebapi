using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qinshift.MovieRent.Services.Interface;

namespace Qinshift.MovieRent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                return Ok(_movieService.GetAllMovies());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return Ok(_movieService.GetMovieById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("queryString")]
        public IActionResult GetByIdQueryString([FromQuery] int id) 
        {
            try
            {
                return Ok(_movieService.GetMovieById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


        [HttpGet("filterMovies")]
        public IActionResult FilterMovies(string genre, int year)
        {
            try
            {
                if(string.IsNullOrEmpty(genre) && year <= 0)
                {
                    return BadRequest("Genre or Year must be provided!");
                }
                return Ok(_movieService.FilterMovies(genre, year));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
