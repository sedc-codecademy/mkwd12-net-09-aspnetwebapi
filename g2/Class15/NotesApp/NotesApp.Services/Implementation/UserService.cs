using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApp.DataAccess.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Dto.UserDto;
using NotesApp.Services.Interfaces;
using NotesApp.Shared.Configuration;
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
        private NotesAppSettings _settings;

        public UserService(IUserRepository userRepository, IOptions<NotesAppSettings> options)
        {
            _userRepository = userRepository;
            // Using the IOptions pattern to retrive the configuration values of the NotesAppSettings
            _settings = options.Value;
        }

        public string LoginUser(LoginUserDto loginUserDto)
        {
            //1. Validation for required fields
            if (string.IsNullOrEmpty(loginUserDto.Username) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            //2.Hash the password   
            string hashPassword = HashPassword(loginUserDto.Password);

            //3. Validate user from db
            User userdb = _userRepository.LoginUser(loginUserDto.Username, hashPassword);
            if (userdb == null)
            {
                throw new UserNotFoundException("User not found!!!");
            }

            //4. GENERATE JWT TOKEN
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our very secret key for noteApp secret new must be 256 characters"); // BAD WAY
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_settings.SecretKey);

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
                            new Claim("userFullName", $"{userdb.FirstName} {userdb.LastName}"),
                            new Claim("userId", userdb.Id.ToString())
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
            var hashPassword = HashPassword(registerUserDto.Password);

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

        private string HashPassword(string password)
        {
            //MD5 hash
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            //Password123@ ---> 123123
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            //hash bytes 12241 --> 4413
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            //get string hashed
            string hashPassword = Encoding.ASCII.GetString(hashBytes);

            return hashPassword;
        }

        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            if (registerUserDto.Username.Length > 50)
            {
                throw new UserDataException("Username: Maximum length for username is 50 characters!!!");
            }

            if (string.IsNullOrEmpty(registerUserDto.FirstName) || string.IsNullOrEmpty(registerUserDto.LastName))
            {
                throw new UserDataException("FirstName and LastName are required fields!");
            }

            if (registerUserDto.FirstName.Length > 100 || registerUserDto.LastName.Length > 100)
            {
                throw new UserDataException("FirstName and LastName: Maximum length is 100 characters!!!");
            }

            if (registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new UserDataException("Password must match!!!");
            }

            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (userDb != null)
            {
                throw new UserDataException($"The username {registerUserDto.Username} already exists!!");
            }
        }

    }
}
