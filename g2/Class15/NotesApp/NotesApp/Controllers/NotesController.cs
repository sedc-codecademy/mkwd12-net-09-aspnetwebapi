using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Dto.NoteDto;
using NotesApp.Services.Interfaces;
using NotesApp.Shared.CustomException;
using System.Security.Claims;

namespace NotesApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService) //DI
        {
            _noteService = noteService;
        }

        [HttpGet]
        public ActionResult<List<NoteDto>> GetAllNotes()
        {
            try
            {
                //get username from token (Name Claim)
                string name = User.Identity?.Name;

                //get the name claim from token second
                string username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

                // Get user id from the User Claims
                var userId = User.FindFirstValue("userId");

                return Ok(_noteService.GetAllNotes(int.Parse(userId)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetNoteById(int id)
        {
            try
            {
                var note = _noteService.GetNoteById(id);
                return Ok(note);
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(ex.Message);
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
                _noteService.AddNote(addNoteDto);
                return StatusCode(StatusCodes.Status201Created, "Note was created!");
            }
            catch (NoteDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateNote([FromBody] UpdateNoteDto updateNoteDto)
        {
            try
            {
                _noteService.UpdateNote(updateNoteDto);
                return NoContent(); //204
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NoteDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteNote(id);
                return Ok($"Note with id: {id} was succ deleted!");
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
