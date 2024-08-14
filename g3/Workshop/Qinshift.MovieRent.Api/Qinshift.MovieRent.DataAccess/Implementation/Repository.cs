using Qinshift.MovieRent.DataAccess.DataSource;
using Qinshift.MovieRent.DataAccess.Interface;
using Qinshift.MovieRent.DomainModels;

namespace Qinshift.MovieRent.DataAccess.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly List<T> _collection;

        public Repository(List<T> collection)
        {
            _collection = collection;
        }
        public IEnumerable<T> GetAll()
        {
            return _collection;
        }
        public T GetById(int id)
        {
            return _collection.SingleOrDefault(x => x.Id == id);
        }
        public int Add(T entity)
        {
            _collection.Add(entity);
            return entity.Id;
        }
        public int Update(T entity)
        {
            var index = _collection.FindIndex(e => e.Id == entity.Id);
            if (index >= 0)
            {
                _collection[index] = entity;
            }
            return entity.Id;
        }
        public int Remove(int id)
        {
            _collection.RemoveAt(id);
            return id;
        }
    }
}
