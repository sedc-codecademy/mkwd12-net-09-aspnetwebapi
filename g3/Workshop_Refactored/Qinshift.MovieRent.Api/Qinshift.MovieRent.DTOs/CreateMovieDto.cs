using Qinshift.MovieRent.DTOs.Enums;

namespace Qinshift.MovieRent.DTOs
{
    public class CreateMovieDto
    {
        public string Title { get; set; }
        public string Plot { get; set; }
        public DateTime ReleaseDate { get; set; }
        public GenreDto Genre { get; set; }
    }
}
