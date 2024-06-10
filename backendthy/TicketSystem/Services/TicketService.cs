using TicketSystem.Models;
using TicketSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TicketSystem.Services
{
    public class TicketService : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ticket BuyTicket(Ticket ticket, List<Passenger> passengers)
        {
            // Get the flight and check capacity
            var flight = _context.Flights.FirstOrDefault(f => f.Id == ticket.FlightId);
            if (flight == null)
            {
                throw new Exception("Flight not found.");
            }

            if (flight.Capacity < passengers.Count)
            {
                throw new Exception("Not enough seats available.");
            }

            // Add the ticket
            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            // Add the passengers
            foreach (var passenger in passengers)
            {
                passenger.TicketId = ticket.Id.ToString(); // Assuming TicketId is a string in Passenger
                _context.Passengers.Add(passenger);
            }

            // Decrease flight capacity
            flight.Capacity -= passengers.Count;
            _context.Flights.Update(flight);
            _context.SaveChanges();

            return ticket;
        }
    }
}
