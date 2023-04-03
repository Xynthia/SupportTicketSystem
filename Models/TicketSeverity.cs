using System.Text.Json.Serialization;

namespace SupportTicketSystem.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TicketSeverity
    {
        Low = 1,
        Normal = 2,
        High = 3,
        Urgent = 4,
        Critical = 5,
    }
}
