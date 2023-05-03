using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Dtos.Ticket;
using SupportTicketSystem.Dtos.UserDtos;
using SupportTicketSystem.Services.ConversationService;
using SupportTicketSystem.Services.JoinUserTicketService.ExtensionMethods;
using SupportTicketSystem.Services.TicketService.ExtensionMethods;

namespace SupportTicketSystem.Services.TicketService
{
    public class TicketService : ITicketService
    {

        public DataContext _dataContext { get; }
        public IMapper _mapper { get; }

        public TicketService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> Add(AddTicketDto newTicket)
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();

            //add new ticket and save it

            Ticket ticket = _mapper.Map<Ticket>(newTicket);

            await _dataContext.Ticket.AddAsync(ticket);
            await _dataContext.SaveChangesAsync();

            serviceResponse = await GetAll();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();

            //delete ticket by id.
            try
            {
                var ticket = await _dataContext.Ticket.FirstAsync(t => t.Id == id);

                _dataContext.Ticket.Remove(ticket);
                await _dataContext.SaveChangesAsync();

                // send mail to user of ticket 
                IConversationService conversationService = null;
                var message = $"\n Hello, \n Your ticket has been deleted. This was the ticket number {ticket.Id}. \n Greetings, {ticket.ResponsibleFor.Name}";
                conversationService.UpdateLog(id, message);

                serviceResponse = await GetAll();
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> GetAll()
        {
            //get all tickets
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();

            serviceResponse.Data = await _dataContext.Ticket.GetTicketDtoFromQuery(_mapper);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDto>> GetById(int id)
        {
            //get ticket by id

            var serviceResponse = new ServiceResponse<GetTicketDto>();

            var ticket = await _dataContext.Ticket.GetById(id);
            serviceResponse.Data = _mapper.Map<GetTicketDto>(ticket);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDto>> Update(int id, UpdateTicketDto updateTicket)
        {
            var serviceResponse = new ServiceResponse<GetTicketDto>();

            //update ticket by id.
            try
            {
                var ticket = await _dataContext.Ticket.GetById(id);
                ticket = _mapper.Map<UpdateTicketDto, Ticket>(updateTicket, ticket);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetTicketDto>(ticket);
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

            //update ticket status
            try
            {
                var ticket = await _dataContext.Ticket.GetById(id);

                ticket.Status = updateTicket.Status;

                await _dataContext.SaveChangesAsync();

                // send mail to user of ticket 
                IConversationService conversationService = null;
                var message = $"\n Hello, \n Your tickets status has changed to {ticket.Status}. \n Greetings, {ticket.ResponsibleFor.Name}";
                conversationService.UpdateLog(id, message);

                serviceResponse.Data = _mapper.Map<GetTicketDto>(ticket);
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
            // add users involved to the tickeet
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();

            var joinUserTicket = _mapper.Map<JoinUserTicket>(newJoinUserTicket);

            await _dataContext.AddAsync(joinUserTicket);
            await _dataContext.SaveChangesAsync();

            // send mail to user of ticket
            IConversationService conversationService = null;
            var message = $"\n Hello, \n Your ticket has more Users involved in the ticket. this user has been added {joinUserTicket.InvolvedUser.Name}. \n Greetings, {joinUserTicket.Ticket.ResponsibleFor.Name}";
            conversationService.UpdateLog(joinUserTicket.TicketId, message);

            serviceResponse.Data = await _dataContext.JoinUserTicket.GetJoinTicketDtoFromQuery(_mapper);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDto>> UpdateSeverityLevelUp(int id)
        {
            var serviceResponse = new ServiceResponse<GetTicketDto>();

            //update the severity level of a ticket 1 level up.
            try
            {
                var ticket = await _dataContext.Ticket.GetById(id);

                int severity = (int)ticket.Severity;
                if (severity < 5)
                {
                    severity = severity + 1;
                }
                var ticketSeverity = (TicketSeverity)severity;

                ticket.Severity = ticketSeverity;

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetTicketDto>(ticket);
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

            //update the severity level of a ticket 1 level down.
            try
            {
                var ticket = await _dataContext.Ticket.GetById(id);

                int severity = (int)ticket.Severity;
                if (severity > 0)
                {
                    severity = severity - 1;
                }
                var ticketSeverity = (TicketSeverity)severity;

                ticket.Severity = ticketSeverity;

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetTicketDto>(ticket);
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

            //update ticket resposible user
            try
            {
                var ticket = await _dataContext.Ticket.GetById(id);

                ticket.ResponsibleForID = userid;

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetTicketDto>(ticket);
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
            // get the user who is the least responsible for tickets.
            var serviceResponse = new ServiceResponse<int>();

            List<User> Users = await _dataContext.User.ToListAsync();
            List<Ticket> Tickets = await _dataContext.Ticket.ToListAsync();
            List<List<int>> AmountResponsibleForPerUser = new List<List<int>>();
            List<int> AmountResponsibleFor = new List<int>();

            int userIdWithLeastAmount = 0;
            
            // looking how many tickets the users are responsible for
            foreach (var user in Users)
            {
                int amountResponsibleFor = 0;
                foreach (var ticket in Tickets)
                {
                    if (ticket.ResponsibleForID == user.Id)
                    {
                        amountResponsibleFor++;
                    }
                }
                AmountResponsibleFor.Add(amountResponsibleFor);


                AmountResponsibleForPerUser.Add(new List<int> { user.Id, amountResponsibleFor });
            }

            AmountResponsibleFor.Sort();

            // getting the user id which has the least amount of tickets responsible for
            foreach (var amountResponsibleForPerUser in AmountResponsibleForPerUser)
            {
                if (amountResponsibleForPerUser.Last() == AmountResponsibleFor.First())
                {
                    userIdWithLeastAmount = amountResponsibleForPerUser.First();

                    serviceResponse.Data = userIdWithLeastAmount;
                    break;
                }
                
            }

            return serviceResponse;
        }
        
    }
}

