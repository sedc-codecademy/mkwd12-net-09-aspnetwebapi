using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DataAccess.Interfaces
{
    //it has all crud methods from IRepository<User> and the additional methods from IUserRepository
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);

        User GetUserByUsernameAndPassword(string username, string password);
    }
}
