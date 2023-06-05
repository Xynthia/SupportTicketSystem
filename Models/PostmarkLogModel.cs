using System.ComponentModel.DataAnnotations;

namespace SupportTicketSystem.Models
{
    public class PostmarkLogModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public string StrippedTextReply { get; set; }
    }
}
