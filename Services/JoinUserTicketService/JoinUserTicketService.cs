using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Services.GuardService;
using SupportTicketSystem.Services.JoinUserTicketService.ExtensionMethods;

namespace SupportTicketSystem.Services.JoinUserTicketService
{
    public class JoinUserTicketService : IJoinUserTicketService
    {
        public MapperlyProfile _mapper { get; }
        public DataContext _dataContext { get; }

        public JoinUserTicketService(MapperlyProfile mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<GetJoinUserTicketDto>>> Add(AddJoinUserTicketDto newJoinUserTicket)
        {
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();

            var joinUserTicket = _mapper.AddJoinUserTicketDtoToJoinUserTicket(newJoinUserTicket);
            Guard.Against.Null(joinUserTicket);

            await _dataContext.AddAsync(joinUserTicket);
            await _dataContext.SaveChangesAsync();

            return serviceResponse = await GetAll();
        }

        public async Task<ServiceResponse<List<GetJoinUserTicketDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();

            try
            {
                var joinUserTicket = await _dataContext.JoinUserTicket.FirstAsync(j => j.Id == id);
                Guard.Against.Null(joinUserTicket);

                //joinUserTicket is being archieved
                joinUserTicket.Archived = DateTime.Now;

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

        public async Task<ServiceResponse<List<GetJoinUserTicketDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();

            var JoinUserTickets = await _dataContext.JoinUserTicket.GetJoinTicketDtoFromQuery(_mapper);
            Guard.Against.Null(JoinUserTickets);

            serviceResponse.Data = JoinUserTickets;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetJoinUserTicketDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetJoinUserTicketDto>();

            var joinUserTicket = await _dataContext.JoinUserTicket.GetById(id);
            Guard.Against.Null(joinUserTicket);
            serviceResponse.Data = _mapper.JoinUserTicketToGetJoinUserTicketDto(joinUserTicket);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetJoinUserTicketDto>> Update(int id, UpdateJoinUserTicketDto updateJoinUserTicket)
        {
            var serviceResponse = new ServiceResponse<GetJoinUserTicketDto>();

            try
            {
                var joinUserTicket = await _dataContext.JoinUserTicket.GetById(id);
                Guard.Against.Null(joinUserTicket);

                joinUserTicket = _mapper.UpdateJoinUserTicketDtoToJoinTicket(updateJoinUserTicket);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.JoinUserTicketToGetJoinUserTicketDto(joinUserTicket);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
