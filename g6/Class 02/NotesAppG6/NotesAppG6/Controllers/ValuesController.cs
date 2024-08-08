using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotesAppG6.Controllers
{
    [Route("api/[controller]")] //http://localhost:[port]/api/Values
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet] //no additional info
        //http://localhost:[port]/api/Values
        public List<string> Get()
        {
            return new List<string>() { "value1", "value2" };
        }

        //[HttpGet]  -> ERROR - has same http method and same address
        ////http://localhost:[port]/api/Values
        //public int GetNumber()
        //{
        //    return 5;
        //}

        [HttpGet("number")]
        //http://localhost:[port]/api/Values/number
        public int GetNumber()
        {
            return 5;
        }

        [HttpPost] //This is okay, beacuse it uses a different http method, although the address is the same
        //http://localhost:[port]/api/Values
        public string GetString()
        {
            return "Hello";
        }

        [HttpGet("details/{id}")]
        //http://localhost:[port]/api/Values/details/1
        public int GetId(int id)
        {
            return id;
        }

        [HttpGet("{id}")]
        //http://localhost:[port]/api/Values/1
        public int GetFirstNumber(int id)
        {
            return id;
        }

        [HttpGet("{userId}/books/{bookId}")]
        //http://localhost:[port]/api/Values/1/books/3
        public string GetUserAndBook(int userId, int bookId)
        {
            return $"User: {userId} - Book: {bookId}";
        }

        [HttpGet("{number}/movies")]
        //http://localhost:[port]/api/Values/3/movies
        public List<string> GetMovies(int number) {
            return new List<string>() { "Deadpool", "Joker" };
        }
    }
}
