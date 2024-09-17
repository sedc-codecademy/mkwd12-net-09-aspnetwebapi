using Qinshift.MovieRent.DomainModels;
using Qinshift.MovieRent.DTOs;

namespace Qinshift.MovieRent.Services.Helpers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(User user)
        {
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
        }

        public static User ToUser(RegisterUserDto user)
        {
            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                ConfirmPassword = user.ConfirmPassword,
            };
        }

    }
}
