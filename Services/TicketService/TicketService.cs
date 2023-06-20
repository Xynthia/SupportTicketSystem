using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Dtos.Ticket;
using SupportTicketSystem.Dtos.UserDtos;
using SupportTicketSystem.Services.ConversationService;
using SupportTicketSystem.Services.GuardService;
using SupportTicketSystem.Services.JoinUserTicketService.ExtensionMethods;
using SupportTicketSystem.Services.TicketService.ExtensionMethods;

namespace SupportTicketSystem.Services.TicketService
{
    public class TicketService : ITicketService
    {
        public DataContext _dataContext { get; }
        public IConversationService _conversationService { get; }
        public MapperlyProfile _mapper { get; }

        public TicketService(DataContext dataContext,  IConversationService conversationService, MapperlyProfile mapper)
        {
            _dataContext = dataContext;
            _conversationService = conversationService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> Add(AddTicketDto newTicket)
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();

            //add new ticket and save it
            var ticket = _mapper.AddTicketDtoToTicket(newTicket);
            Guard.Against.Null(ticket);

            await _dataContext.Ticket.AddAsync(ticket);
            await _dataContext.SaveChangesAsync();

            serviceResponse = await GetAll();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();

            try
            {
                var ticket = await _dataContext.Ticket.FirstAsync(t => t.Id == id);
                Guard.Against.Null(ticket);

                // send mail to user when Ticket is being archived
                var conversation = await _dataContext.Conversation.FirstOrDefaultAsync(c => c.TicketId == ticket.Id);
                Guard.Against.Null(conversation);

                var message = $"\n Hello, \n Your ticket has been deleted. This was the ticket number {ticket.Id}. \n Greetings, {ticket.ResponsibleFor?.Name} \n Secret View";
                await _conversationService.UpdateLog(conversation.Id, message);

                //ticket is being archieved
                ticket.Archived = DateTime.Now;

                await _dataContext.SaveChangesAsync();

                serviceResponse = await GetAll();
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();

            var tickets = await _dataContext.Ticket.GetTicketDtoFromQuery(_mapper);
            Guard.Against.Null(tickets);

            serviceResponse.Data = tickets;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetTicketDto>();

            var ticket = await _dataContext.Ticket.GetById(id);
            Guard.Against.Null(ticket);
            serviceResponse.Data = _mapper.TicketToGetTicketDto(ticket);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDto>> Update(int id, UpdateTicketDto updateTicket)
        {
            var serviceResponse = new ServiceResponse<GetTicketDto>();

            try
            {
                var ticket = await _dataContext.Ticket.GetById(id);
                Guard.Against.Null(ticket);

                ticket = _mapper.UpdateTicketDtoToTicket(updateTicket);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.TicketToGetTicketDto(ticket);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDto>> UpdateStatus(int id, UpdateTicketDto updateTicket)
        {
            var serviceResponse = new ServiceResponse<GetTicketDto>();

            try
            {
                var ticket = await _dataContext.Ticket.GetById(id);
                Guard.Against.Null(ticket);

                ticket.Status = updateTicket.Status;
                ticket.ResponsibleFor = await _dataContext.User.FirstOrDefaultAsync(x => x.Id == ticket.ResponsibleForID);

                // send mail to user of ticket that ticket status has updated 
                var conversation = await _dataContext.Conversation.FirstOrDefaultAsync(c => c.TicketId == ticket.Id);
                Guard.Against.Null(conversation);

                var message = $"\n Hello, \n Your tickets status has changed to {ticket.Status}. \n Greetings, {ticket.ResponsibleFor?.Name} \n Secret View"; 
                await _conversationService.UpdateLog(conversation.Id, message);

                //save changes
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.TicketToGetTicketDto(ticket);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetJoinUserTicketDto>>> AddUsersInvolved(AddJoinUserTicketDto newJoinUserTicket)
        {
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();

            try
            {
                var joinUserTicket = _mapper.AddJoinUserTicketDtoToJoinUserTicket(newJoinUserTicket);
                Guard.Against.Null(joinUserTicket);

                joinUserTicket.InvolvedUser = await _dataContext.User.FirstOrDefaultAsync(u => u.Id == joinUserTicket.UserId);
                joinUserTicket.Ticket = await _dataContext.Ticket.FirstOrDefaultAsync(t => t.Id == joinUserTicket.TicketId);

                await _dataContext.AddAsync(joinUserTicket);
                
                // send mail to user of ticket about that a user has been involved in ticket.
                var conversation = await _dataContext.Conversation.FirstOrDefaultAsync(c => c.TicketId == joinUserTicket.Ticket.Id);
                Guard.Against.Null(conversation);

                var message = $"\n Hello, \n Your ticket has more Users involved in the ticket. this user has been added {joinUserTicket.InvolvedUser.Name}. \n Greetings, {joinUserTicket.Ticket.ResponsibleFor?.Name} \n Secret View";
                await _conversationService.UpdateLog(conversation.Id, message);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = await _dataContext.JoinUserTicket.GetJoinTicketDtoFromQuery(_mapper);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDto>> UpdateSeverityLevelUp(int id)
        {
            var serviceResponse = new ServiceResponse<GetTicketDto>();

            try
            {
                var ticket = await _dataContext.Ticket.GetById(id);
                Guard.Against.Null(ticket);

                int severity = (int)ticket.Severity;
                if (severity < 5)
                {
                    severity = severity + 1;
                }
                var ticketSeverity = (TicketSeverity)severity;

                ticket.Severity = ticketSeverity;

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.TicketToGetTicketDto(ticket);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDto>> UpdateSeverityLevelDown(int id)
        {
            var serviceResponse = new ServiceResponse<GetTicketDto>();

            try
            {
                var ticket = await _dataContext.Ticket.GetById(id);
                Guard.Against.Null(ticket);

                int severity = (int)ticket.Severity;
                if (severity > 0)
                {
                    severity = severity - 1;
                }
                var ticketSeverity = (TicketSeverity)severity;

                ticket.Severity = ticketSeverity;

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.TicketToGetTicketDto(ticket);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDto>> UpdateResposible(int id, int userid)
        {
            var serviceResponse = new ServiceResponse<GetTicketDto>();

            try
            {
                var ticket = await _dataContext.Ticket.GetById(id);
                Guard.Against.Null(ticket);

                ticket.ResponsibleForID = userid;

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.TicketToGetTicketDto(ticket);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> GetLeastAmountResposibleFor()
        {
            var serviceResponse = new ServiceResponse<int>();

            List<Ticket> Tickets = await _dataContext.Ticket.Include(x => x.ResponsibleFor).ToListAsync();
            Guard.Against.Null(Tickets);

            //tickets get grouped by responsiblefor id. ordered by the count of tickets. returns first or default resposible for id.
            var userIdWithLeastAmount = Tickets
                .Where(x => x.ResponsibleFor != null && x.ResponsibleFor.SecretView)
                .GroupBy(x => x.ResponsibleForID)
                .OrderBy(x => x.Count())
                .FirstOrDefault()?.Key;

            Guard.Against.Null(userIdWithLeastAmount);

            serviceResponse.Data = userIdWithLeastAmount.Value;

            return serviceResponse;
        }
        
    }
}

