using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Dtos;

namespace SEDC.MoviesApp.Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetAllMovies(int userId);
        List<MovieDto> FilterMovies(int? year, GenreEnum? genre);
        MovieDto GetMovieById(int id);
        void AddMovie(AddMovieDto addMovieDto, int userId);
        void UpdateMovie(UpdateMovieDto updateMovieDto);
        void DeleteMovie(int id);
    }
}
