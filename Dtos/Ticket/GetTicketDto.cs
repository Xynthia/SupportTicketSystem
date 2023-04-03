namespace SupportTicketSystem.Dtos.Ticket
{
    public class GetTicketDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;
        public TicketSeverity Severity { get; set; }

        public int CreatedByID { get; set; }
        public int ResponsibleForID { get; set; }
    }
}
