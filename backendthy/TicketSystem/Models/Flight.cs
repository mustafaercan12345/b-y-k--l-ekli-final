namespace TicketSystem.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightId { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int Capacity { get; set; }
    }
}
