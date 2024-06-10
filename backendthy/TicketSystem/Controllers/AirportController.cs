using Microsoft.AspNetCore.Mvc;
using TicketSystem.Models;
using TicketSystem.Services;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportService _airportService;

        public AirportController(AirportService airportService)
        {
            _airportService = airportService;
        }

        // POST: api/airport
        [HttpPost]
        public IActionResult AddAirport([FromBody] Airport airport)
        {
            if (airport == null)
            {
                return BadRequest();
            }

            var result =_airportService.AddAirport(airport);
            return CreatedAtAction(nameof(GetAirportById), new { id = airport.Id }, result);
        }

        // GET: api/airport/{id}
        [HttpGet("{id}")]
        public IActionResult GetAirportById(int id)
        {
            var airport = _airportService.GetAirportById(id);

            if (airport == null)
            {
                return NotFound();
            }

            return Ok(airport);
        }

        // GET: api/airport
        [HttpGet]
        public IActionResult GetAirports()
        {
            var airports = _airportService.GetAirports();
            return Ok(airports);
        }
    }
}
