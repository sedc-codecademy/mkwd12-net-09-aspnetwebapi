using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OurFirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("string")]
        public IEnumerable<string> GetStrings(string inputValue)
        {
            return new List<string> { "value1", "value2", "value34", inputValue };
        }
    }
}
