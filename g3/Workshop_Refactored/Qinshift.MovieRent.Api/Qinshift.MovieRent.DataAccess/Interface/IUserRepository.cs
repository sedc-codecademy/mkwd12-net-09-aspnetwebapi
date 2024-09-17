using Qinshift.MovieRent.DomainModels;

namespace Qinshift.MovieRent.DataAccess.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User LoginUser(string username, string password);
    }
}
