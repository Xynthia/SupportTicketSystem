namespace SupportTicketSystem.Services.GuardService
{
    public static class GuardExtensionMethods
    {
        /// <summary>
        /// A method that check if the argument is null.
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="argument"></param>
        /// <param name="exception"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void Null(this IGuardService guardClause ,object argument, string? exception = null)
        {
            if (argument == null)
            {
                var baseException = new ArgumentNullException("Guard xynthia");
                if (baseException != null)
                    throw new InvalidOperationException(exception, baseException);

                throw baseException;
            }
        }
    }
}
