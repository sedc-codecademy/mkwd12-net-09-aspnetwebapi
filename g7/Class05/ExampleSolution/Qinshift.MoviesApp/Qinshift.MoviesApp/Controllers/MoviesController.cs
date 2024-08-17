using Microsoft.AspNetCore.Mvc;
using Qinshift.MoviesApp.DTOs;
using Qinshift.MoviesApp.Models;

namespace Qinshift.MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<MovieDto>> GetAllMovies()
        {
            try
            {
                var moviesDb = StaticDb.Movies;

                return Ok(moviesDb.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                }));
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("{id}")] 
        public ActionResult<MovieDto> GetMovieById([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }

                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound("Movie was not found");
                }

                var movieDto = new MovieDto
                {
                    Description = movieDb.Description,
                    Title = movieDb.Title,
                    Genre = movieDb.Genre,
                    Year = movieDb.Year
                };

                return Ok(movieDto);
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("queryString")] 
        public ActionResult<MovieDto> GetById([FromQuery] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }

                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound("Movie was not found");
                }

                return Ok(new MovieDto
                {
                    Description = movieDb.Description,
                    Title = movieDb.Title,
                    Genre = movieDb.Genre,
                    Year = movieDb.Year
                });
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("filter")]   
        public ActionResult<List<MovieDto>> FilterMoviesFromQuery([FromQuery] int? genre, [FromQuery] int? year)
        {
            try
            {
                if (genre == null && year == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to send at least one filter parameter!");
                }

                if (genre.HasValue)
                //if (genre != null) // same thing
                {
                    // Validate if the value for genre is valid
                    bool isValidGenre = Enum.IsDefined(typeof(Genre), genre.Value);

                    if (!isValidGenre)
                    {
                        return BadRequest("Invalid genre value!");
                    }
                }

                if (year == null)
                {
                    List<Movie> moviesDb = StaticDb.Movies.Where(x => x.Genre == (Genre)genre).ToList();
                    if (!moviesDb.Any())
                    {
                        return NotFound($"Movies of genre {(Genre) genre} not found!");
                    }

                    return Ok(moviesDb.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    }));
                }

                if (genre == null)
                {
                    List<Movie> moviesDb = StaticDb.Movies.Where(x => x.Year == year).ToList();
                    if (!moviesDb.Any())
                    {
                        return NotFound($"Movies for year {year} not found!");
                    }

                    return Ok(moviesDb.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    }));
                }

                List<Movie> movies = StaticDb.Movies.Where(x => x.Year == year
                                                             && x.Genre == (Genre)genre).ToList();
                return Ok(movies.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                }));
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost("addMovie")]
        public IActionResult AddMovie([FromBody] AddMovieDto movieDto)
        {
            try
            {
                if (string.IsNullOrEmpty(movieDto.Title))
                {
                    return BadRequest("Title must not be empty");
                }
                if (!string.IsNullOrEmpty(movieDto.Description) && movieDto.Description.Length > 250)
                {
                    return BadRequest("Description can not be longer than 250 characters");
                }
                if (movieDto.Year <= 0)
                {
                    return BadRequest("Year can not have negative value");
                }

                Movie movie = new Movie()
                {
                    Id = ++StaticDb.MovieId,
                    Year = movieDto.Year,
                    Title = movieDto.Title,
                    Genre = movieDto.Genre,
                    Description = movieDto.Description
                };

                StaticDb.Movies.Add(movie);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPut]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto movie)
        {
            try
            {
                Movie movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == movie.Id);

                if (movieDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, "Resource not found");

                if (string.IsNullOrEmpty(movie.Title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Text must not be empty");
                }
                if (movie.Year <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Year must not be negative");
                }
                if (!string.IsNullOrEmpty(movie.Description) && movie.Description.Length > 250)
                {
                    return BadRequest("Description can not be longer than 250 characters");
                }

                movieDb.Year = movie.Year;
                movieDb.Title = movie.Title;
                movieDb.Description = movie.Description;
                movieDb.Genre = movie.Genre;

                return StatusCode(StatusCodes.Status204NoContent, "Note updated!");
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpDelete]
        public IActionResult DeleteMovie([FromBody] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }

                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound("Movie was not found");
                }

                StaticDb.Movies.Remove(movieDb);

                return StatusCode(StatusCodes.Status204NoContent, "Deleted resource");
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                if (id < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }

                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound("Movie was not found");
                }

                StaticDb.Movies.Remove(movieDb);

                return StatusCode(StatusCodes.Status204NoContent, "Deleted resource");
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

    }
}
