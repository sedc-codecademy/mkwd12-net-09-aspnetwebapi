using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTOs;
using MoviesApp.Models;
using MoviesApp.Models.Enums;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<MovieDto>> GetAll()
        {
            try
            {
                var moviesDb = StaticDb.Movies;

                if (moviesDb == null)
                {
                    return Ok(new List<MovieDto>());
                }

                var moviesDto = moviesDb.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                }).ToList();

                return Ok(moviesDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")] //http://localhost:[port]/api/Movies/1
        public ActionResult<MovieDto> GetByPathId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Bad request, the id cannot be negative number");
                }

                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound($"Movie with id {id} was not found");
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("queryString")] //http://localhost:[port]/api/Movies/queryString?id=1

        public ActionResult<MovieDto> GetMovieByIdWithQueryString(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("The id is a required parameter");
                }

                if (id <= 0)
                {
                    return BadRequest("Bad request, the id cannot be negative number");
                }

                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound($"Movie with id {id} was not found");
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("filter")] //http://localhost:[port]/api/Movies/filter
                            //http://localhost:[port]/api/Movies/filter?genre=1
                            //http://localhost:[port]/api/Movies/filter?year=2024
                            //http://localhost:[port]/api/Movies/filter?genre=1&year=2024
                            //http://localhost:[port]/api/Movies/filter?year=2024&genre=1
        public ActionResult<List<MovieDto>> FilterMovies(int? genre, int? year)
        {
            try
            {
                if (genre == null && year == null)
                {
                    return BadRequest("You have to send at least one filter parameter");
                }

                //genre !=null

                var example = genre.HasValue ? genre.Value : 0;
                var example2 = genre != null ? genre.Value : 0;

                if (genre.HasValue) //HasValue checks if genre is not null (has value)
                {
                    var enumValues = Enum.GetValues(typeof(GenreEnum))
                                      .Cast<GenreEnum>() //Comedy = 1, Action = 2 .....
                                      .Select(g => (int)g) //1,2,3....
                                      .ToList();

                    //in case we send 7 as genre value
                    if (!enumValues.Contains(genre.Value)) //we use .Value to get the value from the int? (int? != int)
                    {
                        return NotFound($"The genre with id {genre.Value} was not found");
                    }
                }

                if(genre == null) //here we can be sure that the year has value, because otherwise the first if would have been true
                {
                    var moviesYear = StaticDb.Movies.Where(x => x.Year == year).ToList();

                    var moviesYearDto = moviesYear.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    }).ToList();

                    return Ok(moviesYearDto);
                }

                // !year.HasValue
                if(year == null) //here we can be sure that the genre has value, because otherwise the first if would have been true
                {
                    List<Movie> moviesGenre = StaticDb.Movies.Where(x => (int)x.Genre == genre.Value).ToList();
                    // List<Movie> moviesGenreEnum = StaticDb.Movies.Where(x => x.Genre == (GenreEnum)genre).ToList();


                    var moviesGenreDto = moviesGenre.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    }).ToList();

                    return Ok(moviesGenreDto);
                }

                List<Movie> movies = StaticDb.Movies.Where(x => x.Year == year && (int)x.Genre == genre.Value).ToList();


                var moviesDto = movies.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                }).ToList();

                return Ok(moviesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("addMovie")]

        public IActionResult AddMovie([FromBody] AddMovieDto addMovieDto)
        {
            try
            {
                //validations
                if (addMovieDto == null)
                {
                    return BadRequest("Movie cannot be null");
                }

                //title is required
                if (string.IsNullOrEmpty(addMovieDto.Title))
                {
                    return BadRequest("Title is required");
                }

                //If description is entered maximum length is 250 characters.
                //description was entered                    and     description had more than 250 characters
                if (!string.IsNullOrEmpty(addMovieDto.Description) && addMovieDto.Description.Length > 250)
                {
                    return BadRequest("Description cannot be longer than 250 characters!");
                }

                if (addMovieDto.Year <= 0 || addMovieDto.Year > DateTime.Now.Year)
                {
                    return BadRequest("Invalid value for year"); //int cannot be null so the check addNoteDto.Year == null would always be false (int? != int)
                }

                var enumValues = Enum.GetValues(typeof(GenreEnum))
                                     .Cast<GenreEnum>() //Comedy = 1, Action = 2 .....
                                     .Select(g => (int)g) //1,2,3....
                                     .ToList();

                if (!enumValues.Contains((int)addMovieDto.Genre)) 
                {
                    return NotFound($"The genre with id {(int)addMovieDto.Genre} was not found");
                }

                //create
                Movie movie = new Movie()
                {
                    Id = StaticDb.Movies.LastOrDefault().Id + 1,
                    Year = addMovieDto.Year,
                    Title = addMovieDto.Title,
                    Genre = addMovieDto.Genre,
                    Description = addMovieDto.Description
                };

                StaticDb.Movies.Add(movie);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto updateMovieDto)
        {
            try
            {
                if(updateMovieDto == null)
                {
                    return BadRequest("Movie cannot be null");
                }

                Movie movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id ==  updateMovieDto.Id);
                if(movieDb == null)
                {
                    return NotFound($"Movie with id {updateMovieDto.Id} was not found");
                }

                //title is required
                if (string.IsNullOrEmpty(updateMovieDto.Title))
                {
                    return BadRequest("Title is required");
                }

                //If description is entered maximum length is 250 characters.
                //description was entered                    and     description had more than 250 characters
                if (!string.IsNullOrEmpty(updateMovieDto.Description) && updateMovieDto.Description.Length > 250)
                {
                    return BadRequest("Description cannot be longer than 250 characters!");
                }

                if (updateMovieDto.Year <= 0 || updateMovieDto.Year > DateTime.Now.Year)
                {
                    return BadRequest("Invalid value for year"); //int cannot be null so the check addNoteDto.Year == null would always be false (int? != int)
                }

                var enumValues = Enum.GetValues(typeof(GenreEnum))
                                     .Cast<GenreEnum>() //Comedy = 1, Action = 2 .....
                                     .Select(g => (int)g) //1,2,3....
                                     .ToList();

                if (!enumValues.Contains((int)updateMovieDto.Genre))
                {
                    return NotFound($"The genre with id {(int)updateMovieDto.Genre} was not found");
                }

                //update
                movieDb.Title = updateMovieDto.Title;
                movieDb.Year = updateMovieDto.Year;
                movieDb.Description = updateMovieDto.Description;
                movieDb.Genre = updateMovieDto.Genre;

                return StatusCode(StatusCodes.Status204NoContent, "Note updated!");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
