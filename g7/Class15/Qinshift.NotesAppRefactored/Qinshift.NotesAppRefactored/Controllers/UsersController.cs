using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qinshift.NotesAppRefactored.Dtos.UserDtos;
using Qinshift.NotesAppRefactored.Services.Interfaces;
using Qinshift.NotesAppRefactored.Shared.CustomExceptions.UserExceptions;
using Serilog;

namespace Qinshift.NotesAppRefactored.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous] // we cannot be logged in before we register and login
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                Log.Information($"Registration model info: FirstName: {registerUserDto.FirstName}, LastName: {registerUserDto.LastName}");
                _userService.RegisterUser(registerUserDto);
                Log.Information($"Successfully registered: {registerUserDto.Username}");
                return StatusCode(StatusCodes.Status201Created, "User successfully created!");
            }
            catch (UserDataException e)
            {
                Log.Error($"There was an error registering the user {registerUserDto.Username} because of the following error: {e.Message}");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Log.Error($"Internal server error happened while registering user {registerUserDto.Username}. Message: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured");
            }
        }

        [AllowAnonymous] // we dont need token here, we cannot be logged in before login
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                string token = _userService.LoginUser(loginUserDto);
                Log.Information($"User {loginUserDto.UserName} has logged in!");
                return Ok(token);
            }
            catch (Exception e)
            {
                Log.Error($"User {loginUserDto.UserName} did not log because of an internal server error");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetById(id);
                Log.Information($"User with id {id} was retrieved from the database");
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound($"User not found! {ex.Message}");
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                Log.Information($"All users were retrieved from the database!");
                return Ok(users);
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser(UpdateUserDto updateUserDto)
        {
            try
            {
                _userService.UpdateUser(updateUserDto);
                Log.Information($"User with id {updateUserDto.Id} was updated in the database");
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound($"User not found! {ex.Message}");
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                Log.Information($"User with id {id} was deleted from the database");
                return StatusCode(StatusCodes.Status204NoContent, "User was successfully deleted!");

            }
            catch (UserNotFoundException ex)
            {
                return NotFound($"User not found! {ex.Message}");
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("test")]
        public string GetTextFromAuthorizedUser()
        {
            return "You can see this text because you are authorized";
        }
    }
}
