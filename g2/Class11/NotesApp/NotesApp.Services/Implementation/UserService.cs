using Microsoft.IdentityModel.Tokens;
using NotesApp.DataAccess.Intefaces;
using NotesApp.Domain.Models;
using NotesApp.Dto.UserDto;
using NotesApp.Services.Interface;
using NotesApp.Shared.CustomException;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace NotesApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string LoginUser(LoginUserDto loginUserDto)
        {
            //1. Validation for required fields
            if(string.IsNullOrEmpty(loginUserDto.Username) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            //2.Hash the password

            //2.1 MD5 hash algorithm
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            //2.2 Password@1 ---> 6123612361
            byte[] passwordByte = Encoding.ASCII.GetBytes(loginUserDto.Password);

            //2.3 get bytes of the hash string 6123612361 --> 123123
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordByte);

            //2.4 get the hash as string 123123 ---> asdasd75576asd7a6ds76asd
            string hashPassword = Encoding.ASCII.GetString(hashBytes); //hasd pw


            //3. Validate user of th db
            User userdb = _userRepository.LoginUser(loginUserDto.Username, hashPassword);
            if(userdb == null)
            {
                throw new UserNotFoundException("User not foud!!!");
            }

            //4. GENERATE JWT TOKEN
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our very secret key for noteApp secret new must be 256 characters");

            // set token
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddDays(1),
                //signature configuration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                //payload
                Subject = new ClaimsIdentity(
                        new[]
                        {
                            new Claim(ClaimTypes.Name, userdb.Username),
                            new Claim("userFullName", $"{userdb.FirstName} {userdb.LastName}")
                            //new Claim(ClaimTypes.Role, userdb.Role)
                        }
                    )
            };

            //generate token
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            //1. VALIDATE
            ValidateUser(registerUserDto);

            //2. hash the password
            //MD5 hash
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            //Password123@ ---> 123123
            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);

            //hash bytes 12241 --> 4413
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            //get string hashed
            string hashPassword = Encoding.ASCII.GetString(hashBytes);

            //3. create user
            User user = new User
            {
                Username = registerUserDto.Username,
                Password = hashPassword, //hashed pass
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName
            };

            _userRepository.Add(user);
        }

        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if(string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            if(registerUserDto.Username.Length > 50)
            {
                throw new UserDataException("Username: Maximum length for username is 50 charactesrs!!!");
            }

            if (string.IsNullOrEmpty(registerUserDto.FirstName) || string.IsNullOrEmpty(registerUserDto.LastName))
            {
                throw new UserDataException("FirstName and LastName are required fields!");
            }

            if(registerUserDto.FirstName.Length > 100 || registerUserDto.LastName.Length > 100)
            {
                throw new UserDataException("FirstName and LastName: Maximum length is 100 charactesrs!!!");
            }

            if(registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new UserDataException("Password must match!!!");
            }

            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if(userDb != null)
            {
                throw new UserDataException($"The username {registerUserDto.Username} already exits!!");
            }

        }
    }
}
