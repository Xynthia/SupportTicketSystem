using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportTicketSystem.Models
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }
        public string Log { get; set; } = string.Empty;

        [ForeignKey("FromUserID")]
        public int FromUserId { get; set; }
        public virtual User FromUser { get; set; }

        [ForeignKey("ToUserID")]
        public int ToUserId { get; set; }
        public virtual User ToUser { get; set; }

        [ForeignKey("TicketId")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
