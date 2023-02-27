﻿namespace SupportTicketSystem.Dtos.UserDtos
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool SecretView { get; set; } = false;
    }
}
