using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAndTagsApp.Models;

namespace NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //http://localhost:[port]/api/Notes
    public class NotesController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/Notes
        public ActionResult<List<Note>> GetAll()
        {
            try
            {
                return Ok(StaticDb.Notes);

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{index}")] //http://localhost:[port]/api/Notes/1
        //Here we have a path variable that is a part of the route
        public ActionResult<Note> GetNoteByIndex(int index)
        {
            try
            {
                if(index < 0)
                {
                    return BadRequest("The index cannot be negative");
                }
                if(index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Notes[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //http://localhost:[port]/api/Notes/queryString
        //http://localhost:[port]/api/Notes/queryString?index=1
        //Here index is a query param - that means it is not a part of the route, but it can be added to the route
        //the index param here is optional
        [HttpGet("queryString")] 
        public ActionResult<Note> GetByQueryString(int? index)
        {
            try
            {
                if(index == null)
                {
                    return BadRequest("The index is required");
                }

                if(index < 0)
                {
                    return BadRequest("The index cannot be negative");
                }

                if(index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Notes[index.Value]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //The parameters here are path params and are part of the route.
        //If we want to change their order, we need to do it directly in the code where we specify the route

        //[HttpGet("{priority}/priority/{text}")]  //http://localhost:[port]/api/Notes/1/priority/gym

        [HttpGet("{text}/priority/{priority}")] //http://localhost:[port]/api/Notes/Gym/priority/1
        public ActionResult<List<Note>> FilterNotes(string text, int priority)
        {
            try
            {
                if(string.IsNullOrEmpty(text) || priority <= 0)
                {
                    return BadRequest("Filter parameters are required");
                }

                if(priority > 3)
                {
                    return BadRequest("Invalid value for priority");
                }

                //text = Gym; priority = 1                                       
                List<Note> filteredNotes = StaticDb.Notes
                       //go to the gym            //gym
                    .Where(x => x.Text.ToLower().Contains(text.ToLower())
                            && (int)x.Priority == priority)
                    .ToList();

                return Ok(filteredNotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("multipleQuery")] //http://localhost:[port]/api/Notes/multipleQuery
                                //http://localhost:[port]/api/Notes/multipleQuery?priority=1
                                //http://localhost:[port]/api/Notes/multipleQuery?text=gym
                                //http://localhost:[port]/api/Notes/multipleQuery?text=gym&priority=1
                                //http://localhost:[port]/api/Notes/multipleQuery?priority=1&text=gym
        public ActionResult<List<Note>> FilterNotesWithQueryParams(string? text, int? priority)
        {
            try
            {
                //validations
                if (string.IsNullOrEmpty(text) && priority == null)
                {
                    return BadRequest("You need to send at least one filter parameter");
                    //another option here is to return all notes
                }
                if (string.IsNullOrEmpty(text)) //&& priority != null
                {
                    if (priority <= 0 ||priority > 3)
                    {
                        return BadRequest("Invalid value for priority");
                    }

                    List<Note> filteredNotesByPriority = StaticDb.Notes
                                                .Where(x => (int)x.Priority == priority)
                                                .ToList();
                    return Ok(filteredNotesByPriority);
                }
                if(priority == null) //&& !string.IsNullOrEmpty(text)
                {
                    List<Note> filteredNotesByText = StaticDb.Notes
                                                        .Where(x => x.Text.ToLower().Contains(text.ToLower()))
                                                        .ToList();
                    return Ok(filteredNotesByText);
                }

                List<Note> filteredNotes = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())
                                                                     && (int)x.Priority == priority)
                                                         .ToList();
                return Ok(filteredNotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreatePostNote([FromBody] Note note)
        {
            try
            {
                if(note == null)
                {
                    return BadRequest("Note cannot be null");
                }

                if (string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("Each note must contain text");
                }

                if(note.Tags == null || note.Tags.Count == 0) //if note.Tags is null or empty list
                {
                    return BadRequest("All notes must have some tags");
                }

                if((int)note.Priority <=0 || (int)note.Priority > 3)
                {
                    return BadRequest("Invalid value for priority");
                }

                StaticDb.Notes.Add(note);
                return StatusCode(StatusCodes.Status201Created, "Note created!");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("language")]
        //in postman we should send key value pair in Headers tab where the key is language
        public IActionResult GetApplicationLanguageFromHeader([FromHeader] string language)
        {
            return Ok(language);
        }

        [HttpGet("userAgent")]
        //in postman we should send key value pair in Headers tab where the key is User-Agent
        public IActionResult GetUserAgentFromHeader([FromHeader(Name ="User-Agent")]string userAgent)
        {
            return Ok(userAgent);
        }
    }
}
