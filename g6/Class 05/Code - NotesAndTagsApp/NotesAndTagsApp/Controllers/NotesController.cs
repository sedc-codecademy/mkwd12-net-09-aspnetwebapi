using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Models;  

namespace NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //http://localhost:[port]/api/Notes
    public class NotesController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/Notes
        public ActionResult<List<NoteDto>> GetAll()
        {
            try
            {
                // return Ok(StaticDb.Notes);

                var notesDb = StaticDb.Notes;

                var notes = notesDb.Select(x => new NoteDto
                {
                    Priority = x.Priority,
                    Text = x.Text,
                    User = $"{x.User.FirstName} {x.User.LastName}",
                    Tags = x.Tags.Select(t => t.Name).ToList()
                }).ToList();

                return Ok(notes);

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpGet("tags")] //http://localhost:[port]/api/Notes
        //public ActionResult<List<Tag>> GetAllTags()
        //{
        //    try
        //    {
        //        // return Ok(StaticDb.Notes);

        //        var tags = StaticDb.Tags;

              
        //        return Ok(tags);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpGet("{id}")] //http://localhost:[port]/api/Notes/1
        //Here we have a path variable that is a part of the route
        public ActionResult<NoteDto> GetNoteById(int id)
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest("The index cannot be negative");
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id); //get from db

                if (noteDb == null)
                {
                    return NotFound($"Note with id {id} does not exist");
                }

                var noteDto = new NoteDto
                {
                    Priority = noteDb.Priority,
                    Text = noteDb.Text,
                    User = $"{noteDb.User.FirstName} {noteDb.User.LastName}",
                    Tags = noteDb.Tags.Select(t => t.Name).ToList()
                };
                return Ok(noteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //http://localhost:[port]/api/Notes/queryString
        //http://localhost:[port]/api/Notes/queryString?id=1
        //Here id is a query param - that means it is not a part of the route, but it can be added to the route
        //the id param here is optional
        [HttpGet("findById")] 
        public ActionResult<NoteDto> FindById(int? id)
        {
            try
            {
                if(id == null)
                {
                    return BadRequest("The index is required");
                }

                if(id < 0)
                {
                    return BadRequest("The index cannot be negative");
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id); //get from db

                if (noteDb == null)
                {
                    return NotFound($"Note with id {id} does not exist");
                }


                var noteDto = new NoteDto
                {
                    Priority = noteDb.Priority,
                    Text = noteDb.Text,
                    User = $"{noteDb.User.FirstName} {noteDb.User.LastName}",
                    Tags = noteDb.Tags.Select(t => t.Name).ToList()
                };
                return Ok(noteDto);
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

        [HttpPut("updateNote/{index}")] //the index param will be sent in the path while the tag will be sent in the body of the request
        public IActionResult AddTagToNote(int index, [FromBody] Tag tag)
        {
            try
            {
                if(index < 0)
                {
                    return BadRequest("The index cannot be negative");
                }

                if(index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no note on index {index}");
                }

                var noteDb = StaticDb.Notes[index];

                if(noteDb.Tags == null) //in case the Tags list is null, we need to create an empty list, otherwise when we call null.Add it will throw an error
                {
                    noteDb.Tags = new List<Tag>();
                }

               noteDb.Tags.Add(tag);
               return StatusCode(StatusCodes.Status204NoContent, "Note updated");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("addNote")]

        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto)
        {
            try
            {
                if(addNoteDto == null)
                {
                    return BadRequest("Note cannot be null");
                }

                if (string.IsNullOrEmpty(addNoteDto.Text))
                {
                    return BadRequest("Each note must contain text!");
                }

                User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == addNoteDto.UserId);
                if(userDb == null) //ex. userId = 73
                {
                    return NotFound($"User with id {addNoteDto.UserId} was not found");
                }

                List<Tag> tags = new List<Tag>();
                //1,2
                foreach(var tagId in addNoteDto.TagIds)
                {
                    Tag tagDb = StaticDb.Tags.FirstOrDefault(x => x.Id == tagId);
                    if(tagId == null)
                    {
                        return NotFound($"Tag with id {tagId} was not found");
                    }

                    tags.Add(tagDb);
                }

                //create
                Note newNote = new Note
                {
                    Id = StaticDb.Notes.LastOrDefault().Id + 1, //we do it like this only because we have not connected to the db yet (it will be genereted there)
                    Text = addNoteDto.Text,
                    Priority = addNoteDto.Priority,
                    User = userDb,
                    UserId = userDb.Id, //addNoteDto.UserId
                    Tags = tags
                };

                StaticDb.Notes.Add(newNote); //write in db

                return StatusCode(StatusCodes.Status201Created, "Note created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("user/{userId}")] //path param

        public ActionResult<List<NoteDto>> GetNotesByUser(int userId)
        {
            try
            {
                //if no notes are founf for the userId, userNotes will be an empty collection []
                var userNotes = StaticDb.Notes.Where(x => x.UserId == userId).ToList();
                var userNotesDto = userNotes.Select(x => new NoteDto
                {
                    Priority = x.Priority,
                    Text = x.Text,
                    User = $"{x.User.FirstName} {x.User.LastName}",
                    Tags = x.Tags.Select(t => t.Name).ToList()
                }).ToList();

                return Ok(userNotesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            try
            {
                //validations
                if(updateNoteDto == null)
                {
                    return BadRequest("Note cannot be null");
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == updateNoteDto.Id);
                if(noteDb == null)
                {
                    return NotFound($"Note with id {updateNoteDto.Id} was not found");
                }

                if (string.IsNullOrEmpty(updateNoteDto.Text))
                {
                    return BadRequest("Each note must contain text");
                }

                User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == updateNoteDto.UserId);
                if (userDb == null) //ex. userId = 73
                {
                    return NotFound($"User with id {updateNoteDto.UserId} was not found");
                }

                List<Tag> tags = new List<Tag>();
                //1,2
                foreach (var tagId in updateNoteDto.TagIds)
                {
                    Tag tagDb = StaticDb.Tags.FirstOrDefault(x => x.Id == tagId);
                    if (tagId == null)
                    {
                        return NotFound($"Tag with id {tagId} was not found");
                    }

                    tags.Add(tagDb);
                }

                //upadte
                noteDb.Text = updateNoteDto.Text;
                noteDb.Priority = updateNoteDto.Priority;
                noteDb.User = userDb;
                noteDb.UserId = userDb.Id; //updateNoteDto.UserId
                noteDb.Tags = tags;

                return StatusCode(StatusCodes.Status204NoContent, "Note updated!");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Id has invalid value!");
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if(noteDb == null)
                {
                    return NotFound($"Note with id {id} was not found");
                }

                StaticDb.Notes.Remove(noteDb);
                return Ok("Note deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
    }
}
