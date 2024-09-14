using Microsoft.EntityFrameworkCore;
using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MoviesApp.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MoviesDbContext _context;
        public UserRepository(MoviesDbContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(x => x.MovieList);
        }

        public User GetById(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }

        public void Update(User update)
        {
            _context.Users.Update(update);
            _context.SaveChanges();
        }
    }
}
