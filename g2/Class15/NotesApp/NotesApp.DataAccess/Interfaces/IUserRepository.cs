using NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User LoginUser(string username, string hasedPassword);
        User GetUserByUsername(string username);
    }
}
