using System.ComponentModel.DataAnnotations;

namespace SupportTicketSystem.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;

        public int? CreatedByID { get; set; }
        public User CreatedBy { get; set; }

        public ICollection<JoinUserTicket> InvolvedUsers { get; set; }

        public ICollection<Conversation> Conversations { get; set; }
    }
}
