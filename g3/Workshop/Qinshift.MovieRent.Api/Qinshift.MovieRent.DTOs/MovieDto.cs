using Qinshift.MovieRent.DTOs.Enums;

namespace Qinshift.MovieRent.DTOs
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Plot { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
    }
}
