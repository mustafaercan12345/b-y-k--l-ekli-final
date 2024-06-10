namespace TicketSystem.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string TicketId { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
    }
}
