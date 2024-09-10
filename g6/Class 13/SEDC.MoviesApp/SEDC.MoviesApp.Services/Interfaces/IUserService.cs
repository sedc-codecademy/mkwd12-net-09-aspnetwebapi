using SEDC.MoviesApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MoviesApp.Services.Interfaces
{
    public interface IUserService
    {
        UserDto Authenticate(LoginModelDto model);
        void Register(RegisterModelDto model);
    }
}
