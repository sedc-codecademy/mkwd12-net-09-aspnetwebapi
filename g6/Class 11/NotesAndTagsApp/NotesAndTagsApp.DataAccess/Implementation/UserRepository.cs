using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DataAccess.Implementation
{
    public class UserRepository : IUserRepository
    {
        private NotesAndTagsAppDbContext _dbContext;

        public UserRepository(NotesAndTagsAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User entity)
        {
          _dbContext.Users.Add(entity);
          _dbContext.SaveChanges();
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
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Username == username);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
