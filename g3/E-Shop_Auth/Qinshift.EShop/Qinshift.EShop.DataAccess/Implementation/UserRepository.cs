using Qinshift.EShop.DataAccess.Interface;
using Qinshift.EShop.DomainModels;

namespace Qinshift.EShop.DataAccess.Implementation
{
    public class UserRepository : DataRepository<User>, IUserRepository
    {
        private readonly EShopDbContext _context;
        public UserRepository(EShopDbContext context) : base(context)
        {
            _context = context;
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return _context.Users.SingleOrDefault(x => x.UserName == username && x.Password == hashedPassword);
        }
    }
}
