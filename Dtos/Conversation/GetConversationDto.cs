namespace SupportTicketSystem.Dtos.Conversation
{
    public class GetConversationDto
    {
        public int Id { get; set; }

        public string Log { get; set; }

        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }

        public int TicketId { get; set; }
    }
}
