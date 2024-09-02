using Qinshift.MovieApp.Domain.Domain;
using Qinshift.MovieApp.Domain.Enums;

namespace Qinshift.MovieApp.Data.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        List<Movie> FilterMovies(int? year, GenreEnum? genre);
    }
}
