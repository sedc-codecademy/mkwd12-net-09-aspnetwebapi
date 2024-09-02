using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Dtos;

namespace SEDC.MoviesApp.Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetAllMovies();
        List<MovieDto> FilterMovies(int? year, GenreEnum? genre);
        MovieDto GetMovieById(int id);
        void AddMovie(AddMovieDto addMovieDto);
        void UpdateMovie(UpdateMovieDto updateMovieDto);
        void DeleteMovie(int id);
    }
}
