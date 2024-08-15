using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qinshift.MovieRent.Services.Interface;

namespace Qinshift.MovieRent.Api.Controllers
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
                if(id <= 0)
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
    }
}
