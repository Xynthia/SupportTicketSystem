namespace SupportTicketSystem.Dtos.Ticket
{
    public class AddTicketDto
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;

        public int CreatedByID { get; set; } 
    }
}
