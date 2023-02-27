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
    }
}
