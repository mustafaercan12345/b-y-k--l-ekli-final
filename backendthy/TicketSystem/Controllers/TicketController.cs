using Microsoft.AspNetCore.Mvc;
using TicketSystem.Models;
using TicketSystem.Services;

namespace TicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }


        [HttpPost("buy")]
        public IActionResult BuyTicket([FromBody] TicketPurchaseRequest request)
        {
            try
            {
                var ticket = new Ticket
                {
                    FlightId = request.FlightId,
                    UserId = request.UserId,
                    PurchaseDate = DateTime.Now,
                    NumberOfPassengers = request.Passengers.Count,
                    IsMilesSmilesPurchase = request.IsMilesSmilesPurchase
                };

                var boughtTicket = _ticketService.BuyTicket(ticket, request.Passengers);
                return Ok(boughtTicket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

    public class TicketPurchaseRequest
    {
        public int FlightId { get; set; }
        public int UserId { get; set; }
        public bool IsMilesSmilesPurchase { get; set; }
        public List<Passenger> Passengers { get; set; }
    }
}
