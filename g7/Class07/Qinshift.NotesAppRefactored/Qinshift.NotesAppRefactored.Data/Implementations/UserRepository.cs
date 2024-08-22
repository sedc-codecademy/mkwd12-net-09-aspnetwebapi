using Qinshift.NotesAppRefactored.Domain.Models;

namespace Qinshift.NotesAppRefactored.Data.Implementations
{
    public class UserRepository : IRepository<User>
    {

        private readonly NotesAppDbContext _notesAppDbContex;

        public UserRepository(NotesAppDbContext notesAppDbContex)
        {
            _notesAppDbContex = notesAppDbContex;
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            return _notesAppDbContex.Users
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
