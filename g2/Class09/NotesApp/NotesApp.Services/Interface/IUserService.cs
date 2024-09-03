using NotesApp.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services.Interface
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto registerUserDto);
        string LoginUser(LoginUserDto loginUserDto);
    }
}
