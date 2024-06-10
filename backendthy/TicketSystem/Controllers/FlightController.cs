using Microsoft.AspNetCore.Mvc;
using TicketSystem.Models;
using TicketSystem.Services;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        // POST: api/flight
        [HttpPost]
        public IActionResult AddFlight([FromBody] Flight flight)
        {
            if (flight == null)
            {
                return BadRequest();
            }

            _flightService.AddFlight(flight);
            return CreatedAtAction(nameof(GetFlightById), new { id = flight.Id }, flight);
        }

        // GET: api/flight/{id}
        [HttpGet("{id}")]
        public IActionResult GetFlightById(int id)
        {
            var flight = _flightService.GetFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        // GET: api/flight/search
        [HttpGet("search")]
        public IActionResult SearchFlights([FromQuery] string departureAirport, [FromQuery] string arrivalAirport, [FromQuery] DateTime departureDate, [FromQuery] int numberOfPassengers)
        {
            var flights = _flightService.SearchFlights(departureAirport, arrivalAirport, departureDate, numberOfPassengers);
            return Ok(flights);
        }
    }
}
