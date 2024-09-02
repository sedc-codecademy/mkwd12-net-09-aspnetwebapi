using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qinshift.NotesAppRefactored.Dtos.UserDtos;
using Qinshift.NotesAppRefactored.Services.Interfaces;
using Qinshift.NotesAppRefactored.Shared.CustomExceptions.UserExceptions;

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
                _userService.RegisterUser(registerUserDto);
                return StatusCode(StatusCodes.Status201Created, "User successfully created!");
            }
            catch (UserDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
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
                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured");
            }
        }
    }
}
