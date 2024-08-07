using Microsoft.AspNetCore.Mvc;

namespace Qinshift.NotesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //http://localhost:[port]/api/books/all-books
        // uncomment the Route() line in order to test this route
        // if you leave it commented, it will fail because of the endpoint on line 27
        [HttpGet] 
        [Route("all-books")]
        public List<string> Get()
        {
            return new List<string> { "book1", "book2", "book3" };
        }

        [HttpGet("info")] //http://localhost:[port]/api/books/info
        public string GetInfo()
        {
            return "This is some info about the book store!";
        }


        // This will not work because there is another route without name
        //[HttpGet]
        //public string GetString()
        //{
        //    return "test";
        //}

        // Get resource by id
        [HttpGet("details/{id}")]
        public string GetById(int id)
        {
            return $"Book with Id: {id}";
        }

        [HttpGet("details/{id}/author/{authorId}")]
        public string GetBookAuthorDetails(int id, int authorId)
        {
            return $"Details about book with Id: {id} and author with Id: {authorId}";
        }

    }
}
