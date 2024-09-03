using Microsoft.EntityFrameworkCore;
using NotesApp.DataAccess.Context;
using NotesApp.DataAccess.Intefaces;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Implementations.EFImplementation
{
    public class UserRepository : IUserRepository
    {
        private NotesAppDbContext _notesAppDbCotext;

        public UserRepository(NotesAppDbContext notesAppDbCotext)
        {
            _notesAppDbCotext = notesAppDbCotext;
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            return _notesAppDbCotext.User.Include(x => x.Notes).FirstOrDefault(x => x.Id == id);
        }

        public void Add(User entity)
        {
            _notesAppDbCotext.User.Add(entity);
            _notesAppDbCotext.SaveChanges();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return _notesAppDbCotext.User.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == hashedPassword);
        }

        public User GetUserByUsername(string username)
        {
            return _notesAppDbCotext.User.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

    }
}
