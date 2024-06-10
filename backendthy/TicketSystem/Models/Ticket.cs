namespace TicketSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int NumberOfPassengers { get; set; }
        public bool IsMilesSmilesPurchase { get; set; }
    }
}
