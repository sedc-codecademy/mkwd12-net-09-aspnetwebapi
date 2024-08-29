using Qinshift.MovieRent.DomainModels;

namespace Qinshift.MovieRent.DataAccess.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Add(T entity);
        int Update(T entity);
        int Remove(int id);
    }
}
