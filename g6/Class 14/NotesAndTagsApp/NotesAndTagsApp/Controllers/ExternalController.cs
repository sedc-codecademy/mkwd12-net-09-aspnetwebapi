using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotesAndTagsApp.DTOs.User;
using System.Text;

namespace NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        [HttpGet]
        public ActionResult<UserDto> GetTestUser()
        {
            //ping TestApi
            //get test user
            //return test user

            //BOTH API-s MUST BE ALIVE, WE MUST RUN THEM!

            try
            {
                using(HttpClient client = new HttpClient())
                {
                    //ping the GET endpoint from Test api
                    HttpResponseMessage response = client.GetAsync("http://localhost:5062/api/Test/testUser").Result;

                    //we need to get the content of the response
                    //the content is in JSON format, that means it is a JSON string
                    string content = response.Content.ReadAsStringAsync().Result;

                    //JSON string -> UserDto

                    UserDto user = JsonConvert.DeserializeObject<UserDto>(content);
                    return Ok(user);

                }

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult PostTestUser(UserDto user)
        {
            try
            {
                using(HttpClient httpClient = new HttpClient())
                {
                    //we need to send our user (UserDto) as json
                    string jsonContent = JsonConvert.SerializeObject(user);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = httpClient.PostAsync("http://localhost:5062/api/Test/addTestUser", content).Result;

                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
