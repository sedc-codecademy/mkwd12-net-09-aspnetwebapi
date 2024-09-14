using Qinshift.NotesAppRefactored.Dtos.UserDtos;

namespace Qinshift.NotesAppRefactored.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto registerUserDto);
        string LoginUser(LoginUserDto loginUserDto);
    }
}
