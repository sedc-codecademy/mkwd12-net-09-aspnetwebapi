using Qinshift.MovieApp.Dtos.UserDtos;

namespace Qinshift.MovieApp.Services.Interfaces
{
    public interface IUserService
    {
        UserDto Authenticate(LoginModelDto model);
        void Register(RegisterModelDto model);
    }
}
