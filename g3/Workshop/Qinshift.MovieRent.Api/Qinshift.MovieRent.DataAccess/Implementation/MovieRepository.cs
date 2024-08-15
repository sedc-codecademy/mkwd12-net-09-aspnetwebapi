using Qinshift.MovieRent.DataAccess.DataSource;
using Qinshift.MovieRent.DataAccess.Interface;
using Qinshift.MovieRent.DomainModels;

namespace Qinshift.MovieRent.DataAccess.Implementation
{
    public class MovieRepository : IRepository<Movie>
    {
        public IEnumerable<Movie> GetAll()
        {
            return StaticDb.Movies;
        }
        public Movie GetById(int id)
        {
            return StaticDb.Movies.SingleOrDefault(x => x.Id == id);
        }
        public int Add(Movie entity)
        {
            StaticDb.Movies.Add(entity);
            return entity.Id;
        }
        public int Update(Movie entity)
        {
            var index = StaticDb.Movies.FindIndex(e => e.Id == entity.Id);
            if (index >= 0)
            {
                StaticDb.Movies[index] = entity;
            }
            return entity.Id;
        }
        public int Remove(int id)
        {
            StaticDb.Movies.RemoveAt(id);
            return id;
        }
    }
}
