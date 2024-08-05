using Microsoft.AspNetCore.Mvc;

namespace Class01.OurFIrstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
