using Qinshift.MovieRent.DTOs;

namespace Qinshift.MovieRent.Services.Interface
{
    public interface IUserService
    {
        int RegisterUser(RegisterUserDto registerUser);
        UserDto Login(LoginDto loginUser);
    }
}
