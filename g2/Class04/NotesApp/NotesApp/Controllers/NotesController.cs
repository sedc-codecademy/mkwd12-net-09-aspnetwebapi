using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.DataAccess;
using NotesApp.Domain.Models;
using NotesApp.Dto.NotesDto;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<NoteDto>> GetAllNotes()
        {
            try
            {
                var notesDb = StaticDb.Notes;
                
                var notes = notesDb.Select(x => new NoteDto()
                {
                    Id = x.Id,
                    Text = x.Text,
                    Priority = x.Priority,
                    User = $"{x.User.FirstName} {x.User.LastName}",
                    Tags = x.Tags
                }).ToList();

                return Ok(notes);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetNoteById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("The id can not be negative!");
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);

                if(noteDb == null)
                {
                    return NotFound($"Note with id {id} does not found");
                }

                var noteDto = new NoteDto()
                {
                    Id = noteDb.Id,
                    Text = noteDb.Text,
                    Priority = noteDb.Priority,
                    User = $"{noteDb.User.FirstName} {noteDb.User.LastName}",
                    Tags = noteDb.Tags
                };

                return StatusCode(StatusCodes.Status200OK, noteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateNote([FromBody] UpdateNoteDto updateNoteDto)
        {
            Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == updateNoteDto.Id);

            if(noteDb == null)
            {
                return NotFound($"Note with id {updateNoteDto.Id} does not found");
            }

            if (string.IsNullOrEmpty(updateNoteDto.Text))
            {
                return BadRequest("Text is a required field!!!");
            }

            User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == updateNoteDto.UserId);

            if (userDb == null)
            {
                return NotFound($"User with id {updateNoteDto.UserId} does not found");
            }

            List<Tag> tags = new List<Tag>();
            foreach(int tagId in updateNoteDto.TagId)
            {
                Tag tagDb = StaticDb.Tags.FirstOrDefault(x => x.Id == tagId);

                if(tagDb == null)
                {
                    return NotFound($"Tag with id {tagId} does not found");
                }

                tags.Add(tagDb);
            }

            //UPDATE
            noteDb.Text = updateNoteDto.Text;
            noteDb.Priority = updateNoteDto.Priority;
            noteDb.User = userDb;
            noteDb.UserId = userDb.Id;
            noteDb.Tags = tags;

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Id has invalid value");
                }
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);

                if (noteDb == null)
                {
                    return NotFound($"Note with id {id} does not found");
                }

                StaticDb.Notes.Remove(noteDb);
                return Ok("Note was deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] AddNoteDto addNoteDto)
        {
            try
            {
                if (string.IsNullOrEmpty(addNoteDto.Text))
                {
                    return BadRequest("Text is a required field");
                }

                User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == addNoteDto.UserId);

                if (userDb == null)
                {
                    return NotFound($"User with id {addNoteDto.UserId} was not found");
                }

                List<Tag> tags = new List<Tag>();
                foreach (int tagId in addNoteDto.TagId)
                {
                    Tag tagDb = StaticDb.Tags.FirstOrDefault(x => x.Id == tagId);

                    if (tagDb == null)
                    {
                        return NotFound($"Tag with id {tagId} does not found");
                    }

                    tags.Add(tagDb);
                }

                //CREATE
                Note newNote = new Note()
                {
                    Id = ++StaticDb.NoteId,
                    Text = addNoteDto.Text,
                    Priority = addNoteDto.Priority,
                    User = userDb,
                    UserId = userDb.Id,
                    Tags = tags
                };

                StaticDb.Notes.Add(newNote);

                return StatusCode(StatusCodes.Status201Created, "Note was created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<NoteDto>> GetNoteByUserId(int userId)
        {
            try
            {
                var userNotes = StaticDb.Notes.Where(x => x.UserId == userId).ToList();

                if (!userNotes.Any())
                {
                    return NotFound($"Note was not found for this user {userId}");
                }

                var userNoteDto = userNotes.Select(x => new NoteDto()
                {
                    Id = x.Id,
                    Priority = x.Priority,
                    Text = x.Text,
                    User = $"{x.User.FirstName} {x.User.LastName}",
                    Tags = x.Tags
                }).ToList();

                return Ok(userNoteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
