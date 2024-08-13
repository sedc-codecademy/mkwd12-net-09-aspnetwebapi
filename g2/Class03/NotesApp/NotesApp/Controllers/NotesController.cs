using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Model;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet] //https://localhost:[port]/api/notes
        public ActionResult<List<Note>> GetAll()
        {
            try
            {
                //return Ok(StaticDb.Notes);
                return StatusCode(StatusCodes.Status200OK, StaticDb.Notes);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{index}")] //https://localhost:[port]/api/notes/1
        public ActionResult<Note> GetNoteByIndex(int index)
        {
            try
            {
                if(index < 0)
                {
                    return BadRequest("The index can not be negative!");
                }
                if(index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Notes[index]);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("queryString")]
        public ActionResult<Note> GetByQueryString(int? index)
        {
            try {
                if (index == null)
                {
                    return BadRequest("Index is a requred parameter");
                }
                if (index < 0)
                {
                    return BadRequest("The index can not be negative!");
                }
                if (index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }
                return Ok(StaticDb.Notes[index.Value]);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("multipleQueryParams")]
        //https://localhost:[port]/api/Notes/multipleQueryParams?text=Drink%20more%20water
        //https://localhost:[port]/api/Notes/multipleQueryParams?priority=2
        //https://localhost:[port]/api/Notes/multipleQueryParams?text=Drink%20more%20water&priority=2
        public ActionResult<List<Note>> FilterByMultipleParams([FromQuery] string? text, [FromQuery] int? priority)
        {
            try
            {
                if(string.IsNullOrEmpty(text) && priority == null)
                {
                    return Ok(StaticDb.Notes);
                }
                if(priority == null)
                {
                    //List<Note> filteredNotes = StaticDb.Notes.Where(x=> x.Text == text).ToList();
                    List<Note> filteredNotes = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())).ToList();

                    return Ok(filteredNotes);
                }

                if (string.IsNullOrEmpty(text))
                {
                    List<Note> filteredNotes = StaticDb.Notes.Where(x => (int)x.Priority == priority).ToList();
                    return Ok(filteredNotes);
                }
                List<Note> notesDb = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower()) && (int)x.Priority == priority).ToList();
                return Ok(notesDb);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("header")]
        public ActionResult<string> GetHeader([FromHeader(Name = "TestHeader")] string testHeader)
        {
            return Ok(testHeader);
        }

        [HttpGet("userAgent")]
        public ActionResult<string> GetHeaderUserAgent([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Ok(userAgent);
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] Note note)
        {
            try
            {
                if (string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("Note text must not be empty");
                }
                if((int)note.Priority > 3 || (int)note.Priority < 0 )
                {
                    return BadRequest("Note priority must not be negative number");
                }
                StaticDb.Notes.Add(note);
                return StatusCode(StatusCodes.Status201Created, "Note was created");

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
