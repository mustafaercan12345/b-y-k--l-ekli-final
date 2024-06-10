using TicketSystem.Models;
using TicketSystem.Repositories;

namespace TicketSystem.Services
{
    public class FlightService : IFlightRepository
    {
        private readonly ApplicationDbContext _context;
        public FlightService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Flight AddFlight(Flight flight)
        {
           _context.Flights.Add(flight);
            _context.SaveChanges();
            return flight;
        }

        public Flight? GetFlightById(int flightId)
        {
           return _context.Flights.FirstOrDefault(a  => a.Id == flightId);
        }

        public IEnumerable<Flight> SearchFlights(string departureAirport, string arrivalAirport, DateTime departureDate, int numberOfPassengers)
        {
            return _context.Flights.ToList();
        }
    }

}
