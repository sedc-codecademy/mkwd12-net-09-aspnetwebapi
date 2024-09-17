using Qinshift.NotesAppRefactored.Dtos.UserDtos;

namespace Qinshift.NotesAppRefactored.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto registerUserDto);
        string LoginUser(LoginUserDto loginUserDto);
        List<UserDto> GetAllUsers();
        UserDto GetById(int id);
        void DeleteUser(int id);
        void UpdateUser(UpdateUserDto userDto);
    }
}
