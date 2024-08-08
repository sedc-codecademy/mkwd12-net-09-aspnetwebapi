using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;

namespace NotesAppG6.Controllers
{
    [Route("api/[controller]")] //http://localhost:[port]/api/Notes
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            // return StatusCode(StatusCodes.Status200OK, StaticDb.SimpleNotes);
            return Ok(StaticDb.SimpleNotes);
        }

        [HttpGet("{index}")] //http://localhost:[port]/api/Notes/1
        public ActionResult<string> GetByIndex(int index)
        {
            try
            {
                //validations
                if (index < 0)
                {
                    // return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value.");
                    return BadRequest("The index has negative value");
                }

                if (index >= StaticDb.SimpleNotes.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no resource on index {index}");
                }
                // throw new Exception("Test exception for status 500");

                return StatusCode(StatusCodes.Status200OK, StaticDb.SimpleNotes[index]);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. Please contact your administrator");
            }
        }

        [HttpGet("{noteId}/user/{userId}")] //http://localhost:[port]/api/Notes/1/user/5
        public ActionResult<string> GetNoteByIdAndUserId(int noteId, int userId)
        {
            try
            {
                if (noteId < 0 || userId < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The ids cannot have negative values");
                    //return BadRequest("The id cannot have negative value");
                }

                return Ok($"Returning note with id {noteId} for user with id {userId}");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
