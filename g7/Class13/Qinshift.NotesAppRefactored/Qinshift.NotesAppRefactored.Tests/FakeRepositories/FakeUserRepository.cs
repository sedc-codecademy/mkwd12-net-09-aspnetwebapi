using Qinshift.NotesAppRefactored.Data;
using Qinshift.NotesAppRefactored.Domain.Models;

namespace Qinshift.NotesAppRefactored.Tests.FakeRepositories
{
    public class FakeUserRepository : IRepository<User>
    {
        //static data for testing purposes
        private List<User> _users;
        public FakeUserRepository() 
        {
            _users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Age = 22,
                    Username = "Boby_123"
                }
            };
        }

        public void Add(User entity)
        {
            _users.Add(entity);
        }

        public void Delete(User entity)
        {
            _users.Remove(entity);
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.SingleOrDefault(user => user.Id == id);
        }

        public void Update(User entity)
        {
            _users[_users.IndexOf(entity)] = entity;
        }
    }
}
