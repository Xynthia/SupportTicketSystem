using System.ComponentModel.DataAnnotations;

namespace SupportTicketSystem.Models
{
    public class JoinUserTicket
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }
        public User InvolvedUser { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
