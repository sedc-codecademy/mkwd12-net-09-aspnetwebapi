using Microsoft.EntityFrameworkCore;
using NotesApp.DataAccess.Context;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Implementations
{
    public class UserRepository : IRepository<User>
    {
        private NotesAppDbContext _notesAppDbCotext;

        public UserRepository(NotesAppDbContext notesAppDbCotext)
        {
            _notesAppDbCotext = notesAppDbCotext;
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
            return _notesAppDbCotext.User.Include(x=>x.Notes).FirstOrDefault(x => x.Id == id);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
