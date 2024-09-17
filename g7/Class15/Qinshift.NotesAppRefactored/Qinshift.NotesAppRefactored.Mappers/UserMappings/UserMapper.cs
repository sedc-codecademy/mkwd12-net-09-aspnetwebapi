using Qinshift.NotesAppRefactored.Domain.Models;
using Qinshift.NotesAppRefactored.Dtos.UserDtos;

namespace Qinshift.NotesAppRefactored.Mappers.UserMappings
{
    public static class UserMapper
    {
        //this keyword will extend to user class so we can use ToUserDto() method on a user object from type User
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Age = user.Age,
            };
        }

        public static User ToUser(this UserDto userDto)
        {
            return new User()
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username= userDto.Username,
                Age= userDto.Age,
            };
        }
    }
}
