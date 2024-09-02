using Qinshift.EShop.DomainModels;

namespace Qinshift.EShop.DataAccess.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User LoginUser(string username, string hashedPassword);
    }
}
