using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Dtos.JoinUserTicket;

namespace SupportTicketSystem.Services.JoinUserTicketService
{
    public class JoinUserTicketService : IJoinUserTicketService
    {
        public JoinUserTicketService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public IMapper _mapper { get; }
        public DataContext _dataContext { get; }

        public async Task<ServiceResponse<List<GetJoinUserTicketDto>>> Add(AddJoinUserTicketDto newJoinUserTicket)
        {
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();
            var joinUserTicket = _mapper.Map<JoinUserTicket>(newJoinUserTicket);
            await _dataContext.AddAsync(joinUserTicket);
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = await _dataContext.JoinUserTicket.Select(j => _mapper.Map<GetJoinUserTicketDto>(j)).ToListAsync();
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetJoinUserTicketDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();
            try
            {
                var joinUserTicket = await _dataContext.JoinUserTicket.FirstOrDefaultAsync(j => j.Id == id);
                _dataContext.Remove(joinUserTicket);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = await _dataContext.JoinUserTicket.Select(j => _mapper.Map<GetJoinUserTicketDto>(j)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetJoinUserTicketDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();
            serviceResponse.Data = await _dataContext.JoinUserTicket.Select(j => _mapper.Map<GetJoinUserTicketDto>(j)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetJoinUserTicketDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetJoinUserTicketDto>();
            var joinUserTicket = await _dataContext.JoinUserTicket.FirstOrDefaultAsync(j => j.Id == id);
            serviceResponse.Data = _mapper.Map<GetJoinUserTicketDto>(joinUserTicket);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetJoinUserTicketDto>> Update(int id, UpdateJoinUserTicketDto updateJoinUserTicket)
        {
            var serviceResponse = new ServiceResponse<GetJoinUserTicketDto>();
            try
            {
                var joinUserTicket = await _dataContext.JoinUserTicket.FirstOrDefaultAsync(j => j.Id == id && updateJoinUserTicket.Id == j.Id);
                joinUserTicket = _mapper.Map<UpdateJoinUserTicketDto, JoinUserTicket>(updateJoinUserTicket, joinUserTicket);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetJoinUserTicketDto>(joinUserTicket);
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
