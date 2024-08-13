using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Qinshift.NotesApp.Controllers
{
    [Route("api/[controller]")] //http://localhost:[port]/api/notes
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/notes
        public ActionResult<List<string>> Get()
        {
            //return StatusCode(StatusCodes.Status200OK, StaticDb.Simplenotes); // this works
            return Ok(StaticDb.Simplenotes); // this works aswell in the same way
        }

        [HttpGet("{index}")] //
        public ActionResult<string> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index must not have negative value");
                }

                if (index >= StaticDb.Simplenotes.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no resourse on index {index}");
                }

                return StatusCode(StatusCodes.Status200OK, StaticDb.Simplenotes[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the administrator");
            }
        }

        [HttpGet("{noteId}/user/{userId}")]
        public ActionResult<string> GetNoteByIdAndUserId(int noteId, int userId)
        {
            if(noteId < 0 || userId < 0)
            {
                //return StatusCode(StatusCodes.Status400BadRequest, "The ids can not be negative!");
                return BadRequest("The ids can not be negative!");
            }

            return Ok($"Returning note with id {noteId} for user with id {userId}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                using(StreamReader reader = new StreamReader(Request.Body))
                {
                    string newNote = reader.ReadToEnd();
                    if (string.IsNullOrEmpty(newNote))
                    {
                        return BadRequest("The body of the request can not be empty");
                    }
                    StaticDb.Simplenotes.Add(newNote);
                    return StatusCode(StatusCodes.Status201Created, "The new note was added");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, Contact the Admin");
            }
        }
    }
}
