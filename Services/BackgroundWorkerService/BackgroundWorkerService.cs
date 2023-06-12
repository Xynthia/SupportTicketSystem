using SupportTicketSystem.Services.GuardService;

namespace SupportTicketSystem.Services.BackgroundWorkerService
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly ILogger<BackgroundService> _logger;
        private readonly IServiceScopeFactory serviceFactory;

        public BackgroundWorkerService(ILogger<BackgroundService> logger, IServiceScopeFactory factory )
        {
            _logger = logger;
            this.serviceFactory = factory;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //update severity level up after 20 sec and assign the ticket to a user when the ticket reaches severity "high".
                try
                {
                    _logger.LogInformation("Time:{time}", DateTimeOffset.Now);

                    await Task.Delay(20000, stoppingToken);

                    //creating scope here because hostedservice is effectively a singleton service and we need to get ticketservice which is scoped.
                    using (var scope = serviceFactory.CreateAsyncScope())
                    {
                        //get all tickets and check if these are null
                        var ticketService = scope.ServiceProvider.GetService<ITicketService>();
                        var ticketServiceResponse = await ticketService.GetAll();
                        Guard.Against.Null(ticketServiceResponse);

                        //for every ticket update severity up
                        foreach (var ticket in ticketServiceResponse.Data)
                        {
                            await ticketService.UpdateSeverityLevelUp(ticket.Id);

                            if (ticket.Severity == TicketSeverity.High)
                            {
                                // get user with least amount of tickets resposible for so the ticket can be added there.
                                if (ticket.ResponsibleForID == null || ticket.ResponsibleForID == 0)
                                {
                                    var userId = await ticketService.GetLeastAmountResposibleFor();

                                    await ticketService.UpdateResposible(ticket.Id, userId.Data);
                                }
                            }
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        $"Failed to execute BackgroundWorkerService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }

    }
}
