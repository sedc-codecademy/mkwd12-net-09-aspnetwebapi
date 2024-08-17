using Qinshift.MoviesApp.Models;

namespace Qinshift.MoviesApp.DTOs
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
    }
}
