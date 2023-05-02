using System.ComponentModel.DataAnnotations;

namespace SupportTicketSystem.Models
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }
        public string Log { get; set; } = string.Empty;

        public int? FromUserId { get; set; }
        public User FromUser { get; set; }

        public int? ToUserId { get; set; }
        public User ToUser { get; set; }

        public int? TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
