using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/first
        public List<string> Get()
        {
            return new List<string>() { "Value 1", "Value 2"};
        }

        [HttpGet("info")] //http://localhost:[port]/api/first/info
        public string GetInfo()
        {
            return "This is our notes app";
        }

        [HttpGet("{id}")] //http://localhost:[port]/api/first/2
        public int GetById(int id)
        {
            return id;
        }
    }
}
