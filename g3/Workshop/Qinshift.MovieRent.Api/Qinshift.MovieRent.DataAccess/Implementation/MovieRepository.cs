using Qinshift.MovieRent.DataAccess.Interface;
using Qinshift.MovieRent.DomainModels;

namespace Qinshift.MovieRent.DataAccess.Implementation
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(List<Movie> collection) : base(collection)
        {

        }
    }
}
