namespace SupportTicketSystem.Dtos.UserDtos
{
    public class AddUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool SecretView { get; set; } = false;
    }
}
