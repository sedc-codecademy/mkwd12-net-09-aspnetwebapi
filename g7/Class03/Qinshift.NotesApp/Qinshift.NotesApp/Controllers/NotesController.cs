using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qinshift.NotesApp.Models;
using System;

namespace Qinshift.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        // ===> Case: No request data
        // https://localhost:7006/api/notes
        // In order to provide more detailed Open API (Swagger) documentation, we can use data annotations (attributes)
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Note>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
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
        // Usecases: Retrieving a specific resource by its identifier (id)
        // https://localhost:7006/api/notes/2
        //public ActionResult<Note> GetNoteByIndex(int index)
        public ActionResult<Note> GetNoteByIndex([FromRoute] int index)
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

        [HttpGet("{text}/priority/{priorityValue}")]
        // ===> Case: Multiple route parametes
        // Example: Find all notes whose text is contained and has priority from the route parameters
        // https://localhost:7006/api/notes/homework/priority/2
        public ActionResult<List<Note>> FilterNotes([FromRoute] string text, [FromRoute] int priorityValue) 
        {
            try
            {
                if (string.IsNullOrEmpty(text) || priorityValue <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Filter parameters are required!");
                }
                if (priorityValue > 3)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Invalid value for priority!");
                }

                List<Note> filteredNotes = StaticDb.Notes.Where(n => n.Text.Contains(text, StringComparison.InvariantCultureIgnoreCase) && (int)n.Priority == priorityValue).ToList();
                return Ok(filteredNotes);
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin!");
            }
        }

        [HttpGet("queryString")]
        // ===> Case: Query (optional) parameter
        // https://localhost:7006/api/notes/queryString?index=1
        public ActionResult<Note> GetByQueryString([FromQuery] int? index)
        {
            try
            {
                if (index is null)
                {
                    return BadRequest("Index is a required parameter!");
                }
                if (index < 0)
                {
                    return BadRequest("The index cannot be negative!");
                }
                if (index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Notes[index.Value]);
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin!");
            }
        }

        [HttpGet("multipleQueryParams")]
        // ===> Case: Multiple Query parameters
        // Usecases: Filtering or sorting data based on user preferences
        // https://localhost:7006/api/notes/multipleQueryParams?text=gym&priority=1
        // https://localhost:7006/api/notes/multipleQueryParams?priority=1
        // https://localhost:7006/api/notes/multipleQueryParams?text=homework
        public ActionResult<List<Note>> FilterNotesByMultipleParams([FromQuery] string? text, [FromQuery] int? priority)
        {
            try
            {
                if (string.IsNullOrEmpty(text) && priority is null)
                {
                    return BadRequest("You have to send at least one filter parameter!");
                }

                List<Note> filteredNotes = new();
                if (string.IsNullOrEmpty(text))
                {
                    // priority has value
                    filteredNotes = StaticDb.Notes.Where(n => (int)n.Priority == priority).ToList();
                    return Ok(filteredNotes);
                }

                if (priority is null)
                {
                    // text has value
                    filteredNotes = StaticDb.Notes.Where(n => n.Text.Contains(text, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    return Ok(filteredNotes);
                }

                // text & priority have values
                filteredNotes = StaticDb.Notes.Where(n => n.Text.Contains(text, StringComparison.InvariantCultureIgnoreCase) && (int)n.Priority == priority).ToList();
                return Ok(filteredNotes);
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin!");
            }
        }

        [HttpGet("userAgent")]
        // ===> Case: Retrieving data from Request Headers
        // Usecases: authorization data, custom defined headers etc.. (metadata)
        // Example: Retrieving header User-Agent (it identifies the client application making the request)
        // https://localhost:7006/api/notes/userAgent
        public IActionResult GetUserAgentHeader([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Ok(userAgent);
        }

        [HttpGet("customHeader")]
        // https://localhost:7006/api/notes/customHeader
        public IActionResult GetHeader([FromHeader(Name = "TestHeader")] string customHeader)
        {
            return Ok(customHeader);
        }

        // ===> Case: Accepting data from the Request Body (Payload)
        // Example: Adding new note to our 'Notes Db'
        // NOTE: Should add apropriate request body
        [HttpPost]
        public IActionResult PostNote([FromBody] Note note)
        {
            try
            {
                if (string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("Note text must not be empty");
                }
                if (note.Tags == null || note.Tags.Count == 0)
                {
                    return BadRequest("Note must contain tags");
                }
                StaticDb.Notes.Add(note);
                return StatusCode(StatusCodes.Status201Created, "Note created");
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin!");
            }
        }

        // ===> Case: Combining more ways (Route Parameter & From Body)
        // Example: Update existing Note with new Tag
        // https://localhost:7006/api/notes/updateNote/2 (From Body: Tag object)
        [HttpPut("updateNote/{noteIndex}")]
        public IActionResult UpdateNote([FromRoute] int noteIndex, [FromBody] Tag newTag) 
        {
            try
            {
                if (noteIndex < 0)
                {
                    return BadRequest("The index cannot be negative!");
                }
                if (noteIndex >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {noteIndex}");
                }

                Note noteDb = StaticDb.Notes[noteIndex];
                noteDb.Tags.Add(newTag);
                return StatusCode(StatusCodes.Status204NoContent, "Note updated!");
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin!");
            }
        }

    }
}
