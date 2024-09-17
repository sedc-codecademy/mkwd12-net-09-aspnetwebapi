using Microsoft.AspNetCore.Mvc;
using Qinshift.MovieRent.DTOs;
using Qinshift.MovieRent.Services.Interface;
using Serilog;

namespace Qinshift.MovieRent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto user)
        {
            try
            {
                var result = _userService.RegisterUser(user);
                if(result > 0)
                {
                    return Ok("User created successfully.");
                }
                return BadRequest("Something went wrong!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Register new user failed.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto user)
        {
            try
            {
                var result = _userService.Login(user);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Something went wrong!");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error while login user.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
