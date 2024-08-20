using MoviesApp.Models;
using MoviesApp.Models.Enums;

namespace MoviesApp
{
    public class StaticDb
    {
        public static List<Movie> Movies = new List<Movie>()
        {
            new Movie
            {
                Id = 1,
                Title = "Bad boys",
                Description = "Comedy action",
                Genre = GenreEnum.Comedy,
                Year = 2024
            },

            new Movie
            {
                Id = 2,
                Title = "James Bond",
                Description = "007",
                Genre = GenreEnum.Action,
                Year = 2022
            }
        };
    }
}
