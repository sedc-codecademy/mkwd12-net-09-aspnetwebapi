using Qinshift.MovieApp.Domain.Enums;

namespace Qinshift.MovieApp.Dtos.MovieDtos
{
    public class AddMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public GenreEnum Genre { get; set; }
    }
}
