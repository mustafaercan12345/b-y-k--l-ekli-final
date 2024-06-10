using Microsoft.AspNetCore.Mvc;
using TicketSystem.Models;
using TicketSystem.Services;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilesSmilesAccountController : ControllerBase
    {
        private readonly MilesSmilesAccountService _milesSmilesAccountService;

        public MilesSmilesAccountController(MilesSmilesAccountService milesSmilesAccountService)
        {
            _milesSmilesAccountService = milesSmilesAccountService;
        }

        // POST: api/milessmilesaccount
        [HttpPost]
        public IActionResult AddMilesSmileAccount([FromBody] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            _milesSmilesAccountService.AddMilesSmileAccount(userId);
            return Ok();
        }

        // PUT: api/milessmilesaccount/{userId}
        [HttpPut("{userId}")]
        public IActionResult UpdateMiles(int userId, [FromBody] int miles)
        {
            if (userId <= 0 || miles < 0)
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                _milesSmilesAccountService.UpdateMiles(userId, miles);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
