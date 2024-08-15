using Microsoft.AspNetCore.Mvc;
using Qinshift.NotesApp.DTOs;
using Qinshift.NotesApp.Models;

namespace Qinshift.NotesApp.Controllers
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
                // 1) Retrive all notes from the StaticDb
                List<Note> notesDb = StaticDb.Notes;

                // 2) Map them to respective DTO model

                // Without using LINQ
                //List<NoteDto> notes = new List<NoteDto>();
                //foreach (var note in notesDb)
                //{
                //    var noteDto = new NoteDto()
                //    {
                //        Priority = note.Priority,
                //        Text = note.Text,
                //        User = $"{note.User.FirstName} {note.User.LastName}"
                //    };
                //    notes.Add(noteDto);
                //}

                List<NoteDto> notes = notesDb.Select(n => new NoteDto
                {
                    Priority = n.Priority,
                    Text = n.Text,
                    User = $"{n.User.FirstName} {n.User.LastName}",
                    Tags = n.Tags.Select(t => t.Name).ToList()
                }).ToList();

                // 3) Return the list of mapped notes
                return Ok(notes);
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong! Contact the admin!");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<NoteDto> GetNoteById([FromRoute] int id)
        {
            try
            {
                // 1) Validation !!!
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The id cannot be negative or zero!");
                }

                // 2) Search for Note with id in our StaticDb
                Note noteDb = StaticDb.Notes.FirstOrDefault(n => n.Id == id);
                if (noteDb is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Note with id {id} does not exist!");
                }

                // 3) Map to Dto and return it
                NoteDto noteDto = new NoteDto
                {
                    Priority = noteDb.Priority,
                    Text = noteDb.Text,
                    User = noteDb.User.GetFullName(),
                    Tags = noteDb.Tags.Select(t => t.Name).ToList()
                };

                return Ok(noteDto);
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong! Contact the admin!");
            }
        }

        //[HttpPost("addNote")]
        [HttpPost]
        public IActionResult AddNote([FromBody] AddNoteDto newNoteDto)
        {
            try
            {
                // 1) Validate the input data 
                if (string.IsNullOrEmpty(newNoteDto.Text))
                {
                    return BadRequest("Text is required!");
                }

                // 2) Find the user by Id who will be assoiciated with the new note
                User userDb = StaticDb.Users.FirstOrDefault(u => u.Id == newNoteDto.UserId);
                if (userDb is null)
                {
                    return NotFound("User not found!");
                }

                // 3) Search for the tags that will be associated with the note
                List<Tag> tags = new List<Tag>();
                foreach (int tagId in newNoteDto.TagIds)
                {
                    Tag tagDb = StaticDb.Tags.FirstOrDefault(t => t.Id == tagId);
                    if (tagDb is null)
                    {
                        return NotFound($"Tag with id {tagId} was not found!");
                    }
                    tags.Add(tagDb);
                }

                // 4) Create the new Note
                Note newNote = new Note
                {
                    Id = StaticDb.GenerateNewId(),
                    Text = newNoteDto.Text,
                    Priority = newNoteDto.Priority,
                    User = userDb,
                    UserId = userDb.Id,
                    Tags = tags
                };

                // 5) Add new note to the static db
                StaticDb.Notes.Add(newNote);

                // 6) Return a status code indicating that the note was successfully created
                return StatusCode(201, "Note created!");
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong! Contact the admin!");
            }
        }

        [HttpPut]
        public IActionResult UpdateNote([FromBody] UpdateNoteDto updateNoteDto)
        {
            try
            {
                // 1) Find the note that we want to update
                Note noteDb = StaticDb.Notes.FirstOrDefault(n => n.Id == updateNoteDto.Id);
                if (noteDb == null)
                {
                    return NotFound("Note not found!");
                }

                // 2) Validate the input data
                if (string.IsNullOrEmpty(updateNoteDto.Text))
                {
                    return BadRequest("Text is a required field!");
                }

                // 3) Find the user whose note is being updated
                User userDb = StaticDb.Users.FirstOrDefault(u => u.Id == updateNoteDto.UserId);
                if (userDb is null)
                {
                    return NotFound("User not found!");
                }

                // 4) Update the note with the new values
                noteDb.Text = updateNoteDto.Text;
                noteDb.Priority = updateNoteDto.Priority;
                noteDb.User = userDb;
                noteDb.UserId = userDb.Id;

                // 5) Return a status code indicating that the operation was successfull, but nothing is returned to the client
                return NoContent();
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong! Contact the admin!");
            }
        }

        [HttpDelete("{noteId}")]
        public IActionResult DeleteById([FromRoute] int noteId)
        {
            try
            {
                // 1) Validate the id
                if (noteId <= 0)
                {
                    return BadRequest("Id has invalid value!");
                }

                // 2) Find the note by ID in the static database
                Note noteDb = StaticDb.Notes.FirstOrDefault(n => n.Id == noteId);
                if (noteDb is null)
                {
                    return NotFound($"Note with id {noteId} was not found!");
                }

                // 3) Remove the note from the Static Db
                StaticDb.Notes.Remove(noteDb);

                return NoContent();
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong! Contact the admin!");
            }
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<NoteDto>> GetNotesByUser(int userId)
        {
            try
            {
                // 1) Retrieve all notes for the specified user 
                List<Note> userNotesDb = StaticDb.Notes.Where(n => n.UserId == userId).ToList();

                // 2) Check if any notes were found
                if (userNotesDb == null || userNotesDb.Count == 0)
                {
                    return NotFound($"Notes for user with id {userId} not found!");
                }

                // 3) Map the notes to DTO
                List<NoteDto> notes = userNotesDb.Select(n => new NoteDto
                {
                    Priority = n.Priority,
                    Text = n.Text,
                    User = n.User.GetFullName(),
                    Tags = n.Tags.Select(t => t.Name).ToList()
                }).ToList();

                // 4) Return the list of mapped notes
                return Ok(notes);
            }
            catch (Exception ex)
            {
                // log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong! Contact the admin!");
            }
        }

    }
}
