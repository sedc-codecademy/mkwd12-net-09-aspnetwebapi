using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MoviesApp.DataAccess
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
