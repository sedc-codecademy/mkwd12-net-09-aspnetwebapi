using Microsoft.EntityFrameworkCore;
using NotesApp.DataAccess.Context;
using NotesApp.DataAccess.Interfaces;
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
            return _notesAppDbCotext.User.ToList();
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
            _notesAppDbCotext.User.Update(entity);
            _notesAppDbCotext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _notesAppDbCotext.User.Remove(entity);
            _notesAppDbCotext.SaveChanges();
        }

        public User LoginUser(string username, string hasedPassword)
        {
            return _notesAppDbCotext.User.FirstOrDefault(x => x.Username == username && x.Password == hasedPassword);
        }

        public User GetUserByUsername(string username)
        {
            return _notesAppDbCotext.User.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }
    }
}
