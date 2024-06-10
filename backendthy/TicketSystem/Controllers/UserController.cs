using Microsoft.AspNetCore.Mvc;
using TicketSystem.Models;
using TicketSystem.Services;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // POST: api/user
        [HttpPost("register")]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user information.");
            }

            bool isExist = _userService.UserExists(user.Email);
            if (isExist)
            {
                return BadRequest("User already exists.");
            }

            var response = _userService.AddUser(user);
            if (response == null)
            {
                return BadRequest("Invalid user information.");
            }
            var token = _userService.GenerateToken();
           return CreatedAtAction(nameof(GetUserById), new { id = response.Id }, new { token, user = response });

        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }



        [HttpGet("login")]
        public IActionResult Login(string email, string password)
        {
            var user = _userService.Login(email,password);

            if (user == null)
            {
                return NotFound();
            }
            var token = _userService.GenerateToken();
            return Ok(new { user = user,token= token});
        }
    }
}
