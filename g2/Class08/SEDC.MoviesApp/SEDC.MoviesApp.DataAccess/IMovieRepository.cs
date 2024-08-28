using SEDC.MoviesApp.Domain;


namespace SEDC.MoviesApp.DataAccess
{
    public interface IMovieRepository : IRepository<Movie>
    {
        List<Movie> FilterMovies(int? year, GenreEnum? genre);
    }
}
