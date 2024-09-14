using Microsoft.IdentityModel.Tokens;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.DTOs.User;
using NotesAndTagsApp.Mappers;
using NotesAndTagsApp.Services.Interfaces;
using NotesAndTagsApp.Shared;
using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

            if (registerUserDto == null)
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

            if (string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmPassword))
            {
                throw new DataException("Password fields are required");
            }

            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new DataException("Passwords must match");
            }

            User userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (userDb != null)
            {
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

        public string Login(LoginUserDto loginUserDto)
        {
            //validations
            if (loginUserDto == null)
            {
                throw new DataException("User cannot be null");
            }

            if (string.IsNullOrEmpty(loginUserDto.Username) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new DataException("Username and password are required");
            }

            string hash = GenerateHash(loginUserDto.Password);

            //we send the hashed password, because that is what we keep in the db and what we need to compare
            User userDb = _userRepository.GetUserByUsernameAndPassword(loginUserDto.Username, hash);
            if (userDb == null)
            {
                throw new Exception($"Invalid login for user with username: {loginUserDto.Username}");
            }

            //generate JWT token that will be returned to the client
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our very secret secretttt secret key");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddHours(5), //the token will be valid for 5 hours

                //signing configuration, signing algorithm that will be used to generate the hash
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                                        SecurityAlgorithms.HmacSha256Signature),

                //payload
                Subject = new System.Security.Claims.ClaimsIdentity(
                new[]
                {
                    //things that will be stored in the token about the user
                    new Claim("userFullName", userDb.Firstname + ' ' + userDb.Lastname),
                    new Claim(ClaimTypes.NameIdentifier, userDb.Username),
                    new Claim("userRole", "Admin") //  new Claim("userRole", userDb.Role)
                }
                )
            };

            //generate token
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            //convert to string (here we have SecurityToken and we need string)
            string resultToken = jwtSecurityTokenHandler.WriteToken(token);

            return resultToken;
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
