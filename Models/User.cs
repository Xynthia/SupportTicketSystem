using System.ComponentModel.DataAnnotations;

namespace SupportTicketSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool SecretView { get; set; } = false;

        public DateTime? Archived { get; set; } = null;

        public ICollection<Ticket> CreatedTickets { get; set; }
        public ICollection<JoinUserTicket> JoinUserTicket { get; set; }
        public ICollection<Conversation> Conversations { get; set; }

    }
}
