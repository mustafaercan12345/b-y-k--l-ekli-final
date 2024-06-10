using TicketSystem.Models;

namespace TicketSystem.Repositories
{
    public interface IAirport
    {
        Airport? GetAirportById(int airportId);
        IEnumerable<Airport> GetAirports();
        Airport AddAirport(Airport airport);
    }
}
