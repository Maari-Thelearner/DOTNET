namespace TicketProcessor
{
    public class Ticket
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsEmailSent { get; set; }
    }
}
