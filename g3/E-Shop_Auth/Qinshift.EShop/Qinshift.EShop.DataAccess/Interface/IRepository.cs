using Qinshift.EShop.DomainModels;

namespace Qinshift.EShop.DataAccess.Interface
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
