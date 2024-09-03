using Microsoft.IdentityModel.Tokens;
using Qinshift.MovieApp.Data.Interfaces;
using Qinshift.MovieApp.Domain.Domain;
using Qinshift.MovieApp.Mappers;
using Qinshift.MovieApp.Dtos.UserDtos;
using Qinshift.MovieApp.Services.Interfaces;
using Qinshift.MovieApp.Shared.CustomExceptions.UserException;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace Qinshift.MovieApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserDto Authenticate(LoginModelDto model)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(model.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);
            var user = _userRepository.GetAll().SingleOrDefault(x =>
                x.Username == model.Username && x.Password == hashedPassword);
            if (user == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Our very secret secret key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userModel = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = tokenHandler.WriteToken(token),
                MovieList = user.MovieList.Select(movie => movie.ToMovieDto()).ToList()
            };
            return userModel;
        }

        public void Register(RegisterModelDto model)
        {
            // Validations
            if (string.IsNullOrEmpty(model.FirstName))
                throw new UserException(null, model.Username,
                    "First name is required");
            if (string.IsNullOrEmpty(model.LastName))
                throw new UserException(null, model.Username,
                    "Last name is required");
            if (!ValidUsername(model.Username))
                throw new UserException(null, model.Username,
                    "Username is already in use");
            if (model.Password != model.ConfirmPassword)
                throw new UserException(null, model.Username,
                    "Passwords did not match!");

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(model.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = hashedPassword
            };

            _userRepository.Add(user);


        }

        private bool ValidUsername(string username)
        {
            return _userRepository.GetAll().All(x => x.Username != username);
        }
    }
}
