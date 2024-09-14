﻿using Qinshift.NotesAppRefactored.Domain.Models;
using Qinshift.NotesAppRefactored.Dtos.UserDtos;

namespace Qinshift.NotesAppRefactored.Mappers.UserMappings
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this UserDto user)
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