using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OurFirstApi.Controllers
{
    [Route("api/[controller]")] //http://[port]/api/Values
    //it refers to all actions in the controller
    [ApiController]
    public class ValuesController : ControllerBase 
    {
        [HttpGet] //no additional info
        //http://[port]/api/Values
        public IEnumerable<string> GetStrings()
        {
            return new List<string> { "value1", "value2" };

        }

        [HttpPost]   //http://[port]/api/Values
        public string Post()
        {
            return "OK";
        }

        [HttpGet("string")]  //http://[port]/api/Values/string
        public string GetString()
        {
            return "value3";
        }

        //[HttpGet] //http://[port]/api/Values   -> ERROR - has same http method and same address
        //public string GetInfo()
        //{
        //    return "This is our values controller";
        //}

        [HttpGet("info")] //http://[port]/api/Values/info
        public string GetInfo()
        {
            return "This is our values controller";
        }

        //http://[port]/api/Values/details/id  - it will not be like this
        //http://[port]/api/Values/details/1 - the route will look something like this - the id will be replaced by the value of id
        [HttpGet("details/{id}")]   
        public int GetId(int id)
        {
            return id;
        }
    }
}
