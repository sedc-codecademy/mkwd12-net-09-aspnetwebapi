using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Dto.UserDto;
using NotesApp.Services.Interfaces;
using NotesApp.Shared.CustomException;
using Serilog;

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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                _userService.RegisterUser(registerUserDto);
                return StatusCode(StatusCodes.Status201Created, "User created!");
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
