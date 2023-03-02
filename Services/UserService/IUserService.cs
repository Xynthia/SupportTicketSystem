using SupportTicketSystem.Dtos.Ticket;
using SupportTicketSystem.Dtos.UserDtos;

namespace SupportTicketSystem.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAll();
        Task<ServiceResponse<GetUserDto>> GetById(int id);
        Task<ServiceResponse<List<GetUserDto>>> Add(AddUserDto newUser);
        Task<ServiceResponse<GetUserDto>> Update(int id, UpdateUserDto updateUser);
        Task<ServiceResponse<List<GetUserDto>>> Delete(int id);
        Task<ServiceResponse<List<GetTicketDto>>> GetAllTickets(int id);
        Task<ServiceResponse<GetUserDto>> UpdateSecretView(int id, bool secretview);
        
    }
}
