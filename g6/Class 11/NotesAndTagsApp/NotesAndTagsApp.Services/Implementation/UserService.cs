using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.DTOs.User;
using NotesAndTagsApp.Mappers;
using NotesAndTagsApp.Services.Interfaces;
using NotesAndTagsApp.Shared;
using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            //1. validation data
            //-if we have first name and last name, they should not contain more than 50 characters
            //-username must be sent and it can have max 30 characters
            //-password must be sent
            //-password and confirmed password must match
            //-username must be unique, there should be no other user with the same username

            if(registerUserDto == null)
            {
                throw new DataException("User cannot be null");
            }

            ValidationHelper.ValidateRequiredStringColumnLength(registerUserDto.Username, "Username", 30);

            if (!string.IsNullOrEmpty(registerUserDto.Firstname))
            {
                ValidationHelper.ValidateStringColumnLength(registerUserDto.Firstname, "Firstname", 50);
            }

            if (!string.IsNullOrEmpty(registerUserDto.Lastname))
            {
                ValidationHelper.ValidateStringColumnLength(registerUserDto.Lastname, "Lastname", 50);
            }

            if(string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmPassword))
            {
                throw new DataException("Password fields are required");
            }

            if(registerUserDto.Password != registerUserDto.ConfirmPassword) {
                throw new DataException("Passwords must match");
            }

            User userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if(userDb != null) {
                //this means that we have a user with registerUserDto.Username in the db
                throw new DataException($"Username {registerUserDto.Username} is already in use");
            }

            //2. add record to db
            //2.1 get domain model from the dto: RegisterUserDto -> User
            // hash the password ex. Test123 -> hsfj325hr

            string hash = GenerateHash(registerUserDto.Password);

            User newUser = registerUserDto.ToUser(hash);

            _userRepository.Add(newUser);
        }

        private static string GenerateHash(string password)
        {
            //MD5 
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            //from string to byte: Test123 -> 5678432
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            //from bytes into hash bytes: 5678432 -> 6512431
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            //get the string from hash bytes:  6512431 -> qsw54fg
            string hash = Encoding.ASCII.GetString(hashedBytes);

            return hash;
        }
    }
}
