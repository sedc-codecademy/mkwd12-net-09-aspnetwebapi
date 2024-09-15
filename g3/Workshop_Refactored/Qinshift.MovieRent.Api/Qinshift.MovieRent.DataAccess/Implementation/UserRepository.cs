using Microsoft.EntityFrameworkCore;
using Qinshift.MovieRent.DataAccess.Interface;
using Qinshift.MovieRent.DomainModels;

namespace Qinshift.MovieRent.DataAccess.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly MovieRentDbContext _conext;
        public UserRepository(MovieRentDbContext context) : base(context)
        {
            _conext = context;
        }

        public User LoginUser(string userName, string password)
        {
            return _conext.Users.SingleOrDefault(x => x.UserName == userName && x.Password == password);
        }
    }
}
