using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qinshift.NotesAppAndTags.Db;
using Qinshift.NotesAppAndTags.Models;
using System.IO.Pipes;

namespace Qinshift.NotesAppAndTags.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<Note>> Get()
        {
            try
            {
                return Ok(StaticDb.Notes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin");
            }
        }

        [HttpGet("{index}")]
        public ActionResult<Note> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                    return BadRequest("Index must have positive value!");
                if (index > StaticDb.Notes.Count)
                    return NotFound($"Cannot find resource on index {index}");

                return StaticDb.Notes[index - 1];
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin");
            }
        }

        [HttpGet("getByQueryString")]
        public ActionResult<Note> GetByQueryString(int index)
        {
            try
            {
                if (index < 0)
                    return BadRequest("Index must have positive value!");
                if (index > StaticDb.Notes.Count)
                    return NotFound($"Cannot find resource on index {index}");

                return StaticDb.Notes[index - 1];
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin");
            }
        }

        [HttpGet("{text}/priority/{priority}")]
        public ActionResult<List<Note>> FilterNotes(string text, int priority)
        {
            try
            {
                if (string.IsNullOrEmpty(text) || priority <= 0)
                    return BadRequest("Filter paramteres are required!");
                if (priority > 3)
                    return BadRequest("Invalid value for priority!");

                var notes = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower()) && priority == (int)x.Priority).ToList();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin");
            }
        }

        [HttpGet("multipleParams")]
        public ActionResult<List<Note>> FilterNotesByMultipleParams(string? text, int? priority)
        {
            try
            {
                if (string.IsNullOrEmpty(text) && priority == null)
                    return BadRequest("You have to send at least one parameter");

                if(string.IsNullOrEmpty(text))
                {
                    List<Note> filteredNotes = StaticDb.Notes.Where(x => (int)x.Priority == priority).ToList();
                    return Ok(filteredNotes);
                };

                if(priority == null)
                {
                    List<Note> filteredNotes = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())).ToList();
                    return Ok(filteredNotes);
                }

                var notes = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower()) && priority == (int)x.Priority).ToList();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured! Contact the admin");
            }
        }
    }
}
