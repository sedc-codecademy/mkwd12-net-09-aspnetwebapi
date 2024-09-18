using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet("testUser")]
        public ActionResult<User> GetTestUser()
        {
            User user = new User()
            {
                Firstname = "Test",
                Lastname = "User",
                Username = "testUser"
            };

            return Ok(user);
        }

        [HttpPost("addTestUser")]
        public IActionResult AddTestUser([FromBody] User user)
        {
            return Ok();
        }
    }
}
