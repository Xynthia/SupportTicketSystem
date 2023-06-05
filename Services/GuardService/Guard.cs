namespace SupportTicketSystem.Services.GuardService
{
    public class Guard : IGuardService
    {
        public static IGuardService Against { get; } = new Guard();

        private Guard() { }
    }
}
