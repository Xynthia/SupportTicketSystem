namespace SupportTicketSystem.Dtos.Conversation
{
    public class GetConversationDto
    {
        public int Id { get; set; }
        public string Log { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public int? FromUserId { get; set; }

        public int TicketId { get; set; }
    }
}
