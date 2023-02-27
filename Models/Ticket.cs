using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportTicketSystem.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;

        [ForeignKey("CreatedBy")]
        public int CreatedBy { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("InvolvedUsers")]
        public int InvolvedUsers { get; set; }
        public virtual JoinUserTicket JoinUserTicket { get; set; }
    }
}
