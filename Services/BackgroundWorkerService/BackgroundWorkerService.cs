using AutoMapper;
using SupportTicketSystem.Data;

namespace SupportTicketSystem.Services.BackgroundWorkerService
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly ILogger<BackgroundService> _logger;
        private readonly IServiceScopeFactory serviceFactory;

        public BackgroundWorkerService(ILogger<BackgroundService> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            this.serviceFactory = factory;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Time:{time}", DateTimeOffset.Now);

                    await Task.Delay(20000, stoppingToken);

                    using (var scope = serviceFactory.CreateAsyncScope())
                    {
                        var ticketService = scope.ServiceProvider.GetService<ITicketService>();
                        var userService = scope.ServiceProvider.GetService<IUserService>();

                        var ticketServiceResponse = await ticketService.GetAll();

                        foreach (var ticket in ticketServiceResponse.Data)
                        {
                            await ticketService.UpdateSeverityLevelUp(ticket.Id);

                            if (ticket.Severity == TicketSeverity.High)
                            {
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
