using TicketSystem.Models;
using TicketSystem.Repositories;

namespace TicketSystem.Services
{
    public class AirportService : IAirport
    {
        private readonly ApplicationDbContext _context;

        public AirportService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Airport AddAirport(Airport airport)
        {
            _context.Airports.Add(airport);
            _context.SaveChanges();
            return airport;
        }

        public Airport? GetAirportById(int airportId)
        {
            return _context.Airports.FirstOrDefault(a => a.Id == airportId);
        }

        public IEnumerable<Airport> GetAirports()
        {
            return _context.Airports.ToList();
        }
    }
}
