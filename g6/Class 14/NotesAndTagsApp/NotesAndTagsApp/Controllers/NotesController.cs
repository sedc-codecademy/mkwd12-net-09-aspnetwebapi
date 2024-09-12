using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotesAndTagsApp.DTOs.Notes;
using NotesAndTagsApp.Services.Interfaces;
using NotesAndTagsApp.Shared.CustomExceptions;
using Serilog;
using System.Security.Claims;

namespace NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        //[Authorize] //user must be logged in to access this method (must send a token)
        [HttpGet]
        public ActionResult<List<NoteDto>> GetAll()
        {
            try
            {
                //var identity = HttpContext.User.Identity as ClaimsIdentity;

                //if(identity.FindFirst("userRole").Value != "Admin")
                //{
                //    return StatusCode(StatusCodes.Status403Forbidden);
                //}

               // throw new Exception("Our error");

                return Ok(_noteService.GetAllNotes());
            }
            catch (Exception ex)
            {
                Log.Error("An error occured");
                string exceptionString = JsonConvert.SerializeObject(ex);
                Log.Error(exceptionString);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetById(int id)
        {
            try
            {
                var noteDto = _noteService.GetById(id);
                return Ok(noteDto);
            }
            catch(NoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //note can be added only from user with role admin or superAdmin
        [Authorize] //we must know the user
        [HttpPost]
        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto)
        {
            try
            {
                //we must validate if the user is with role admin or superAdmin

                //var identity = HttpContext.User.Identity as ClaimsIdentity;

                //if (identity.FindFirst("userRole").Value != "Admin" && identity.FindFirst("userRole").Value != "superAdmin")
                //{
                //    return StatusCode(StatusCodes.Status403Forbidden);
                //}

                string role = User.Claims.First(x => x.Type == "userRole").Value; //type is our key of the Claim(that we configured in UserService (login method)
                if(role != "Admin" && role != "superAdmin")
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

                Log.Information("User authorized");

                _noteService.AddNote(addNoteDto);

                Log.Information("Note added");

                return StatusCode(StatusCodes.Status201Created, "Note created");
            }
            catch(DataException ex) //catch our custom made exception
            {
                Log.Error("Data related error occured");
                string exceptionString = JsonConvert.SerializeObject(ex);
                Log.Error(exceptionString);

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("An error occured");
                string exceptionString = JsonConvert.SerializeObject(ex);
                Log.Error(exceptionString);

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
            catch (DataException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteNote(id);
                return Ok($"Note with id {id} was successfully deleted");
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
