using Microsoft.IdentityModel.Tokens;
using Qinshift.NotesAppRefactored.Data.Interfaces;
using Qinshift.NotesAppRefactored.Domain.Models;
using Qinshift.NotesAppRefactored.Dtos.UserDtos;
using Qinshift.NotesAppRefactored.Services.Interfaces;
using Qinshift.NotesAppRefactored.Shared.CustomExceptions.UserExceptions;
using Qinshift.NotesAppRefactored.Mappers.UserMappings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;


namespace Qinshift.NotesAppRefactored.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new UserNotFoundException("user was not found!");

            _userRepository.Delete(user);
        }

        public List<UserDto> GetAllUsers()
        {
            //first we are getting all the users from our database
            List<User> users = _userRepository.GetAll();

            //we need a userDto list since we are always returning dto models, note domain ones
            var userDtos = new List<UserDto>();

            //we need to map the domain User models to UserDto models and put then in the new list
            foreach (var user in users)
            {
                //for every user in our users list we are going to map it into userDto
                var mappedUser = user.ToUserDto();
                //we need to add the mapped UserDto into the userDtos list
                userDtos.Add(mappedUser);
            }

            //we just need to return the new userDtos mapped list
            return userDtos;
        }

        public UserDto GetById(int id)
        {

            User user = _userRepository.GetById(id);
            if (user == null)
                throw new UserNotFoundException("user was not found!");

            UserDto userDto = user.ToUserDto();
            return userDto;
        }

        public string LoginUser(LoginUserDto loginUserDto)
        {
            //1. Validation
            if(string.IsNullOrWhiteSpace(loginUserDto.UserName) || string.IsNullOrEmpty(loginUserDto.Password)) 
            {
                throw new UserDataException("Username and password fields are required fields!");
            }

            //2. Hash the password with MD5 hash algorithm
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //3. converting the value from the password to bytes
            byte[] passwordBytes = Encoding.ASCII.GetBytes(loginUserDto.Password);

            //4. hashing the bytes 
            byte[] hashBytes = md5.ComputeHash(passwordBytes);

            //converting the hashed bytes to string
            string hash = Encoding.ASCII.GetString(hashBytes);

            //try to get the user
            User userDb = _userRepository.LoginUser(loginUserDto.UserName, hash);
            if(userDb == null)
            {
                throw new UserNotFoundException("User not found!");
            }

            //GENERATE THE JWT TOKEN
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our very secret key");

            //information about the token =>
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1), // the token will be valid for 1 hour
                //signature configuration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                //payload
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}")
                        //new Claim("role", userDb.Role)
                    }
                )
            };

            //generate token
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            //convert the token to string
            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            //1. Validate
            ValidateUser(registerUserDto);

            //2. hash the password
            //MD5 hash algoritm
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            //3. converting the value of the password to bytes
            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);

            //4. hashing the bytes
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            //5. convert the hash bytes into string
            string hash = Encoding.ASCII.GetString(hashBytes);

            //6. we create the user and add it to our Db
            User user = new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Password = hash
            };
            _userRepository.Add(user);
        }

        public void UpdateUser(UpdateUserDto updateUserDto)
        {
            User user = _userRepository.GetById(updateUserDto.Id);

            if (user == null)
                throw new UserNotFoundException($"User with id {updateUserDto.Id} was not found");

            user.Username = updateUserDto.Username;
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Age = updateUserDto.Age;
            _userRepository.Update(user);
        }

        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if(string.IsNullOrEmpty(registerUserDto.Username) || 
               string.IsNullOrEmpty(registerUserDto.Password) ||
               string.IsNullOrEmpty(registerUserDto.ConfirmedPassword)) 
            {
                throw new UserDataException("Username and password are required fields!");
            }

            if(registerUserDto.Username.Length > 30)
            {
                throw new UserDataException("Username: Maximun length for username is 30 charachters");
            }
            if(!string.IsNullOrEmpty(registerUserDto.FirstName) && registerUserDto.FirstName.Length > 50) 
            {
                throw new UserDataException("Maximum length for Firstname is 50 characters");
            }
            if (!string.IsNullOrEmpty(registerUserDto.LastName) && registerUserDto.LastName.Length > 50)
            {
                throw new UserDataException("Maximum length for Lastnamename is 50 characters");
            }
            if(registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new UserDataException("Passwords do not match");
            }

            //chech if there is an existing username in our db
            //because lets say we want users usernames to be unique
            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if(userDb != null)
            {
                throw new UserDataException($"The username {registerUserDto.Username} is already in use");
            }
        }
    }
}
