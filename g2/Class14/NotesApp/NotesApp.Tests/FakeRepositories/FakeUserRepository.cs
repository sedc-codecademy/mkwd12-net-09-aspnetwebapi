using NotesApp.DataAccess.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.Tests.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        private List<User> _users;

        public FakeUserRepository()
        {
            _users = new List<User>() 
            {
                new User
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Age = 23,
                    Username = "bob123"
                }
            };
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public User LoginUser(string username, string hasedPassword)
        {
            throw new NotImplementedException();
        }

    }
}
