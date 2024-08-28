using Qinshift.MovieRent.DTOs;

namespace Qinshift.MovieRent.Services.Interface
{
    public interface IMovieService
    {
        List<MovieDto> GetAllMovies();
        MovieDto GetMovieById(int id);
        int AddNewMovie(CreateMovieDto movie);
        int UpdateMovie(CreateMovieDto movie, int id);
        int DeleteMovie(int id);
        List<MovieDto> FilterMovies(string genre, int year);
    }
}
