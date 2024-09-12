using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Dto.UserDto;
using NotesApp.Services.Interfaces;
using NotesApp.Shared.CustomException;
using Serilog;
using System.Diagnostics;

namespace NotesApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous] //no token needed
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                Debug.WriteLine("Log something in Output window");
                Log.Information("Processing login attempt for user: {Username}", loginUserDto.Username);
                string token = _userService.LoginUser(loginUserDto);
                Log.Information("User successfully logged in: {Username}", loginUserDto.Username);
                return Ok(token);
            }
            catch (UserDataException ex)
            {
                Log.Error(ex, "Login failed for user: {Username}. Reason: {Message}", loginUserDto.Username, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                Log.Warning("Login failed for user: {Username}. Reason: {Message}", loginUserDto.Username, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occured while logging in user: {User}. Message {Message}", loginUserDto.Username, ex.Message);
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
                // TIP: It's considered good practice to retrive generic error message where unexpected errors may occure (both for security and user experience)
            }

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                Log.Information("Processing registration for new user: {Username}", registerUserDto.Username);
                _userService.RegisterUser(registerUserDto);
                Log.Information("User registered successfully: {Username}", registerUserDto.Username);
                return StatusCode(StatusCodes.Status201Created, "User created!");
            }
            catch (UserDataException ex)
            {
                Log.Warning("Registration failed for user: {Username}. Reason: {Message}", registerUserDto.Username, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occured while registering in user: {User}. Message {Message}", registerUserDto.Username, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }
    }
}
