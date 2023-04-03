namespace SupportTicketSystem.Dtos.Ticket
{
    public class UpdateTicketDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; }
        public TicketSeverity Severity { get; set; }

        public int CreatedByID { get; set; }

        public int ResponsibleForID { get; set; }


    }
}
