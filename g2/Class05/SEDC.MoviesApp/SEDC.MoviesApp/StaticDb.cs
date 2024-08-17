using SEDC.MoviesApp.Models;

namespace SEDC.MoviesApp
{
    public static class StaticDb
    {
        public static int MovieId = 2;
        public static List<Movie> Movies = new List<Movie>()
        {
            new Movie
            {
                Id = 1,
                Title = "Top Gun:Maverick",
                Description = "Action movie",
                Genre = GenreEnum.Action,
                Year = 2022
            },

             new Movie
            {
                Id = 2,
                Title = "Dumb and dumber",
                Description = "Action movie",
                Genre = GenreEnum.Comedy,
                Year = 1994
            }

        };
    }
}
