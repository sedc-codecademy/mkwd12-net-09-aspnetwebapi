

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Dto.UserDto;
using NotesApp.Services.Interface;

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
            string token = _userService.LoginUser(loginUserDto);
            return Ok(token);
        }
    }
}
