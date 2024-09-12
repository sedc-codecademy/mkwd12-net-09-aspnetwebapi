using NotesAndTagsApp.DTOs.User;
using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserDto registerUserDto, string hash)
        {
            return new User
            {
                Firstname = registerUserDto.Firstname,
                Lastname = registerUserDto.Lastname,
                Username = registerUserDto.Username,
                //Password = registerUserDto.Password // password must not be kept as plain text!!
                Password = hash
            };
        }
    }
}
