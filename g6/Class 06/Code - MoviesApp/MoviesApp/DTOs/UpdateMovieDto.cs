using MoviesApp.Models.Enums;

namespace MoviesApp.DTOs
{
    public class UpdateMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public GenreEnum Genre { get; set; }
    }
}
