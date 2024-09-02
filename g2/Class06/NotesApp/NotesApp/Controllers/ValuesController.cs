using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpDelete("{index}")]
        public IActionResult DeleatUsername(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value. Please try again.");
                }
                if (index > 4)
                if (index >= StaticDb.Usernames.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no resource on index {index}");
                }
                StaticDb.Usernames.Remove(StaticDb.Usernames[index]);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

    public static class StaticDb
    {
        public static List<string> Usernames { get; set; } = new List<string>();
    }
}
