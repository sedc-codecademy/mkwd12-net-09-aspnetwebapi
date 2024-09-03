using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Intefaces
{
    public interface IUserRepository : IRepository<User>
    {
        User LoginUser(string username, string hashedPassword);
        User GetUserByUsername(string username);
    }
}
