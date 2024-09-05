using Qinshift.NotesAppRefactored.Data.Interfaces;
using Qinshift.NotesAppRefactored.Domain.Models;

namespace Qinshift.NotesAppRefactored.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly NotesAppDbContext _notesAppDbContex;

        public UserRepository(NotesAppDbContext notesAppDbContex)
        {
            _notesAppDbContex = notesAppDbContex;
        }

        public void Add(User entity)
        {
            _notesAppDbContex.Users.Add(entity);
            _notesAppDbContex.SaveChanges();
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

        public User GetUserByUsername(string username)
        {
            return _notesAppDbContex.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return _notesAppDbContex.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower()
                   && x.Password == hashedPassword);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
