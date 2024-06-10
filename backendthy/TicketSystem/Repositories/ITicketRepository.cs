using TicketSystem.Models;

namespace TicketSystem.Repositories
{
    public interface ITicketRepository
    {
        Ticket BuyTicket(Ticket ticket, List<Passenger> passengers);
    }
}
