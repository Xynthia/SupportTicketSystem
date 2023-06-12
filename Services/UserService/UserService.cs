using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Dtos.Ticket;
using SupportTicketSystem.Dtos.UserDtos;
using SupportTicketSystem.Services.GuardService;
using SupportTicketSystem.Services.JoinUserTicketService.ExtensionMethods;
using SupportTicketSystem.Services.TicketService.ExtensionMethods;
using SupportTicketSystem.Services.UserService.ExtensionMethods;

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
            // add user and save
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            
            var user = _mapper.Map<User>(newUser);
            Guard.Against.Null(user);

            await _dataContext.User.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return serviceResponse = await GetAll();
        }

        public async Task<ServiceResponse<List<GetUserDto>>> Delete(int id)
        {
            // delete user by id
            var serviceRepsonse = new ServiceResponse<List<GetUserDto>>();
            try
            {
                var user = await _dataContext.User.FirstAsync(u => u.Id == id);
                Guard.Against.Null(user);

                user.Archived = DateTime.Now;

                await _dataContext.SaveChangesAsync();

                serviceRepsonse = await GetAll();
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceRepsonse.Succes = false;
                serviceRepsonse.Message = ex.Message;
            }
            return serviceRepsonse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAll()
        {
            var serviceRespone = new ServiceResponse<List<GetUserDto>>();

            var Users = await _dataContext.User.GetUserDtoFromQuery(_mapper);
            Guard.Against.Null(Users);

            serviceRespone.Data = Users;

            return serviceRespone;
        }

        public async Task<ServiceResponse<GetUserDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            var user = await _dataContext.User.GetById(id);
            Guard.Against.Null(user);

            serviceResponse.Data = _mapper.Map<GetUserDto>(user);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> Update(int id, UpdateUserDto updateUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _dataContext.User.GetById(id);
                Guard.Against.Null(user);
                user = _mapper.Map<UpdateUserDto, User>(updateUser, user);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
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
                var user = await _dataContext.User.GetById(id);
                Guard.Against.Null(user);

                user.SecretView = secretview;
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> GetAllCreatedTickets(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();
            
            List<Ticket> tickets = await _dataContext.Ticket.GetByCreatedId(id);
            Guard.Against.Null(tickets);

            serviceResponse.Data = _mapper.Map<List<GetTicketDto>>(tickets);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetJoinUserTicketDto>>> GetAllInvolvedTickets(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();

            List<JoinUserTicket> involvedUsers = await _dataContext.JoinUserTicket.GetByUserId(id);
            Guard.Against.Null(involvedUsers);

            serviceResponse.Data = _mapper.Map<List<GetJoinUserTicketDto>>(involvedUsers);

            return serviceResponse;
        }
    }
}
