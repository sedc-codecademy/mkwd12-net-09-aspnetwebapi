using Qinshift.MovieRent.DomainModels;
using Qinshift.MovieRent.DomainModels.Enums;
using Qinshift.MovieRent.DTOs;

namespace Qinshift.MovieRent.Services.Helpers
{
    public static class MovieMapper
    {
        public static MovieDto ToMovieDto(Movie movie)
        {
            if (movie != null)
            {
                return new MovieDto
                {
                    Title = movie.Title,
                    Plot = movie.Plot,
                    ReleaseDate = movie.ReleaseDate,
                    Genre = movie.Genre.ToString()
                };
            }
            return null;
        }

        public static Movie ToMovie(CreateMovieDto createMovieDto)
        {
            return new Movie
            {
                Title = createMovieDto.Title,
                Plot = createMovieDto.Plot,
                ReleaseDate = createMovieDto.ReleaseDate,
                Genre = (Genre)createMovieDto.Genre
            };
        }
    }
}
