using Microsoft.IdentityModel.Tokens;
using Qinshift.EShop.DataAccess.Interface;
using Qinshift.EShop.DomainModels;
using Qinshift.EShop.DTOs.User;
using Qinshift.EShop.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Qinshift.EShop.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserLoginResponseDto LoginUser(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Username and password must be provided");
            }

            // 1. Hash the password

            MD5 md5CryptoService = MD5.Create();

            // Test123 -> dasWcS31W
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            byte[] hashBytes = md5CryptoService.ComputeHash(passwordBytes);

            string hashedPassword = Encoding.ASCII.GetString(hashBytes);

            // 2. Call Login method from user repository

            var user = _userRepository.LoginUser(username, hashedPassword);


            // 3. Validate if the user exists
            if(user == null)
            {
                throw new Exception("User not found!");
            }

            // 4. Configure Token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(1),
                // Configure signature
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                                            SecurityAlgorithms.HmacSha256Signature),
                // Payload
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("userFullName", $"{user.FirstName}{user.LastName}")
                    })
            };

            // 5. Generate Token
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            // 6. Return the Token

            var userModel = new UserLoginResponseDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Token = tokenHandler.WriteToken(token) // Converting from SecurityToken object to string
            };

            return userModel;
        }

        public int RegisterUser(RegisterUserDto registerUserDto)
        {
            if(string.IsNullOrEmpty(registerUserDto.UserName) ||
                string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new Exception("Username and password must be provided!");
            }

            MD5 md5CryptoServiceProvider = MD5.Create();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);

            byte[] hashBytes = md5CryptoServiceProvider.ComputeHash(passwordBytes);

            string hashedPassword = Encoding.ASCII.GetString(hashBytes);

            User user = new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                UserName = registerUserDto.UserName,
                Password = hashedPassword
            };

            return _userRepository.Add(user);
        }
    }
}
