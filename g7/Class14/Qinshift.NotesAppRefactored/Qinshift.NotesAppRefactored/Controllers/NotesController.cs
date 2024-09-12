using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qinshift.NotesAppRefactored.Dtos.NoteDtos;
using Qinshift.NotesAppRefactored.Services.Interfaces;
using Qinshift.NotesAppRefactored.Shared.CustomExceptions.NoteExceptions;
using Serilog;

namespace Qinshift.NotesAppRefactored.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService) // DI
        {
            _noteService = noteService;
        }

        [HttpGet]
        public ActionResult<List<NoteDto>> GetAll()
        {
            try
            {
                return Ok(_noteService.GetAllNotes());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetById(int id)
        {
            try
            {
                var noteDto = _noteService.GetById(id);
                //Log.Information($"Note with id {id} was fetched!");
                Log.Information($"Retrieved note ${noteDto.Text}");
                return Ok(noteDto); // status code 200/ok
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto)
        {
            try
            {
                _noteService.AddNote(addNoteDto);
                //Not best practice to provide the user id in the log => just for demo purposes
                Log.Information($"Note with text {addNoteDto.Text} was added from user {addNoteDto.UserId}");
                return StatusCode(StatusCodes.Status201Created, "Note Created");
            }
            catch (NoteDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateNote([FromBody] UpdateNoteDto updateNoteDto)
        {
            try
            {
                _noteService.UpdateNote(updateNoteDto);
                return NoContent(); // 204
            }
            catch (NoteDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteNote(id);
                return Ok($"Note with id {id} successfully deleted!");
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
