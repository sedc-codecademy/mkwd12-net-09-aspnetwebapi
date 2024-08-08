using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")] //http://localhost:[port]/api/notes
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/notes
        public ActionResult<List<string>> GetAll()
        {
            //return Ok(StaticDb.SimpleNotes);
            return StatusCode(StatusCodes.Status200OK, StaticDb.SimpleNotes);
        }

        [HttpGet("{index}")] //https://localhost:44316/api/notes/2
        public ActionResult<string> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value. Please try again.");
                }
                if (index >= StaticDb.SimpleNotes.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no resource on index {index}");
                }
                return StatusCode(StatusCodes.Status200OK, StaticDb.SimpleNotes[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }
        [HttpGet("{noteId}/user/{userId}")] //https://localhost:44316/api/notes/2/user/3
        public ActionResult<string> GetNoteByIdAndUserId(int noteId, int userId)
        {
            if(noteId < 0 || userId < 0)
            {
                return BadRequest("The ids can not be negative!");
            }

            return Ok($"NoteId: {noteId} --- UserId: {userId}");
        }

        [HttpPost] //https://localhost:44316/api/notes
        public IActionResult CreateNote([FromBody] string newNote)
        {
            try
            {
                if (string.IsNullOrEmpty(newNote))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The body of the request can not be empty");
                }
                StaticDb.SimpleNotes.Add(newNote);
                return StatusCode(StatusCodes.Status201Created, "The new note was created!");
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteNote([FromBody] string note)
        {
            if (string.IsNullOrEmpty(note))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "The body of the request can not be empty");
            }
            if (!StaticDb.SimpleNotes.Contains(note))
            {
                return StatusCode(StatusCodes.Status404NotFound, "The note was not found");
            }

            StaticDb.SimpleNotes.Remove(note);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
