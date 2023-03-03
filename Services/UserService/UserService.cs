using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Dtos.Ticket;
using SupportTicketSystem.Dtos.UserDtos;

namespace SupportTicketSystem.Services.UserService
{
    public class UserService : IUserService
    {
        public IMapper _mapper { get; }
        public DataContext _dataContext { get; }

        public UserService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> Add(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            
            User user = _mapper.Map<User>(newUser);

            await _dataContext.User.AddAsync(user);

            await _dataContext.SaveChangesAsync();

            serviceResponse.Data = await _dataContext.User.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> Delete(int id)
        {
            var serviceRepsonse = new ServiceResponse<List<GetUserDto>>();
            try
            {
                var user = await _dataContext.User.FirstAsync(u => u.Id == id);
                _dataContext.User.Remove(user);
                await _dataContext.SaveChangesAsync();
                serviceRepsonse.Data = await _dataContext.User.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceRepsonse.Succes = false;
                serviceRepsonse.Message = ex.Message;
            }
            return serviceRepsonse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAll()
        {
            var serviceRespone = new ServiceResponse<List<GetUserDto>>();
            serviceRespone.Data = await _dataContext.User.Select(c => _mapper.Map<GetUserDto>(c)).ToListAsync();
            return serviceRespone;
        }

        public async Task<ServiceResponse<GetUserDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var user = await _dataContext.User.FirstOrDefaultAsync(u => u.Id == id);
            serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> Update(int id, UpdateUserDto updateUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _dataContext.User.FirstOrDefaultAsync(u => u.Id == updateUser.Id && u.Id == id);

                user = _mapper.Map<UpdateUserDto, User>(updateUser, user);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateSecretView(int id, bool secretview)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _dataContext.User.FirstOrDefaultAsync(u => u.Id == id);

                user.SecretView = secretview;

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> GetAllCreatedTickets(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();
            
            List<Ticket> tickets = await _dataContext.Ticket.Where(t => id == t.CreatedByID).ToListAsync();

            serviceResponse.Data = _mapper.Map<List<GetTicketDto>>(tickets);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetJoinUserTicketDto>>> GetAllInvolvedTickets(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();

            List<JoinUserTicket> involvedUsers = await _dataContext.JoinUserTicket.Where(t => id == t.UserId).ToListAsync();

            serviceResponse.Data = _mapper.Map<List<GetJoinUserTicketDto>>(involvedUsers);

            return serviceResponse;
        }


    }
}
