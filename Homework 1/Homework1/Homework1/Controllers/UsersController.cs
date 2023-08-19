using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Add new UsersController
    //Add static list of strings in a static db, containing user names
    public class UsersController : ControllerBase
    {
        //Add GET method that returns all users
        [HttpGet]
        public ActionResult<List<string>> GetAllUsernames()
        {
            return Ok(StaticDb.Usernames);
        }

        //Add GET method that returns one user
        [HttpGet("{index}")]
        public ActionResult<string> GetUsers(int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index can not be negative");
                }

                if (index >= StaticDb.Usernames.Count())
                {
                    return NotFound("There is no such user.");
                }

                return Ok(StaticDb.Usernames[index]);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Try again, an error occurred.");
            }
        }
    }
}
