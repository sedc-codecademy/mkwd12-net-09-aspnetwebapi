using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Qinshift.NotesApp.Controllers
{
    [Route("api/[controller]")] //http://localhost:[port]/api/values
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet] // we dont have additional route here when calling this method
        //http://localhost:[port]/api/values
        public List<string> Get() 
        {
            return new List<string>() { "value1", "value2" };
        }

        //http://localhost:[port]/api/values/info
        [HttpGet("info")] 
        public string GetInfo()
        {
            return "This is our notes app";
        }

        ////Has Same http method and same adress as the first one and it will throw an error
        //[HttpGet]
        ////http://localhost:[port]/api/values
        //public string GetString()
        //{
        //    return "test";
        //}

        //ALLOWED
        //This is allowed on the same address because the method is different
        [HttpPost]
        public string Post()
        {
            return "Ok";
        }

        [HttpGet("details/{id}")] //http:localhost:[port]/api/values/details/1 or any given number that we provide
        public int GetById(int id)
        {
            return id;
        }
    }
}
