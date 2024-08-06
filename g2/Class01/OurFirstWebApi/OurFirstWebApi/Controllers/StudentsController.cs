using Microsoft.AspNetCore.Mvc;

namespace OurFirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        // api/students
        public List<string> Get()
        {
            var students = new List<string>() { "Bob Bobsky", "John Doe", "Jane Doe", "Greg Gregsky" };
            return students;
        }

        [HttpGet("grades")]
        // api/students/grades
        public List<int> GetStudentsGrades()
        {
            return new List<int>() { 1, 5, 3 };
        }
    }
}
