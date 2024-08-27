namespace Qinshift.NotesAppRefactored.Data
{
    public interface IRepository<T>
    {
        //CRUD OPERATIONS
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
