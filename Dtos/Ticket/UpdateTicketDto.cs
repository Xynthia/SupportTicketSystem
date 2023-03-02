namespace SupportTicketSystem.Dtos.Ticket
{
    public class UpdateTicketDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;

        public int CreatedByID { get; set; }
    }
}
