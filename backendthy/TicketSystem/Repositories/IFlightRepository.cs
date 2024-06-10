using TicketSystem.Models;

namespace TicketSystem.Repositories
{
    public interface IFlightRepository
    {
        Flight? GetFlightById(int flightId);
        IEnumerable<Flight> SearchFlights(string departureAirport, string arrivalAirport, DateTime departureDate, int numberOfPassengers);
        Flight AddFlight(Flight flight);
    }
}
