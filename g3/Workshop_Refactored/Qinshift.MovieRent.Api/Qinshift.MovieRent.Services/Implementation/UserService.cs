using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Qinshift.MovieRent.DataAccess.Interface;
using Qinshift.MovieRent.DomainModels;
using Qinshift.MovieRent.DTOs;
using Qinshift.MovieRent.Services.Helpers;
using Qinshift.MovieRent.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Qinshift.MovieRent.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<AppSettings> _options;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,
            IOptions<AppSettings> options,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _options = options;
            _mapper = mapper;
        }

        public UserDto Login(LoginDto loginUser)
        {
            var hashedPassword = HashPassword(loginUser.Password);
            var user = _userRepository.LoginUser(loginUser.UserName, hashedPassword);
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            //var userResult = UserMapper.ToUserDto(user);
            var userResult = _mapper.Map<UserDto>(user);

            userResult.Token = GenerateToken(user);

            return userResult;
        }

        public int RegisterUser(RegisterUserDto registerUser)
        {
            ValidateUser(registerUser);
            var hashedPassword = HashPassword(registerUser.Password);
            registerUser.Password = hashedPassword;
            registerUser.ConfirmPassword = hashedPassword;
            var userDomain = UserMapper.ToUser(registerUser);
            return _userRepository.Add(userDomain);
        }

        private static void ValidateUser(RegisterUserDto user)
        {
            if (user.Password != user.ConfirmPassword)
                throw new Exception("Password did not match.");
        }

        private static string HashPassword(string password)
        {
            MD5 md5CryptoServiceProvider = MD5.Create();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5CryptoServiceProvider.ComputeHash(passwordBytes);
            string hashedPassword = Encoding.ASCII.GetString(hashBytes);

            return hashedPassword;
        }

        private string GenerateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //byte[] secretKeyBytes = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                                            SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                new[]
                {
                        new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("userFullName", $"{user.FirstName}{user.LastName}")
                    })
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
