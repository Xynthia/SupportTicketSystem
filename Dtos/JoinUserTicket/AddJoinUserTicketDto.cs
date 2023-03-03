namespace SupportTicketSystem.Dtos.JoinUserTicket
{
    public class AddJoinUserTicketDto
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int TicketId { get; set; }
    }
}
