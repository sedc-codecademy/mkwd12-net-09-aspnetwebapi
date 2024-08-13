using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qinshift.NotesApp.Models;

namespace Qinshift.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        // https://localhost:7006/api/notes
        public ActionResult<List<Note>> GetNotes()
        {
            try
            {
                // return Ok(StaticDb.Notes); // First approach
                return StatusCode(StatusCodes.Status200OK, StaticDb.Notes); // Second approach
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin!");
            }
        }

        [HttpGet("{index}")]
        // ===> Case: Route parameter
        // https://localhost:7006/api/notes/2
        public ActionResult<Note> GetNoteByIndex(int index)
        {
            try
            {
                // NOTE: When dealing with request data be sure to make proper validations !!!
                if (index < 0)
                {
                    return BadRequest("The index cannot be negative !");
                }
                if (index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Notes[index]);
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin!");
            }
        }
    }
}
