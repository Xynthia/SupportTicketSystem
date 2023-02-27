using SupportTicketSystem.Dtos.UserDtos;

namespace SupportTicketSystem.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAll();
        Task<ServiceResponse<GetUserDto>> GetById(int id);
        Task<ServiceResponse<List<GetUserDto>>> Add(AddUserDto newUser);
        Task<ServiceResponse<GetUserDto>> Update(UpdateUserDto updateUser);
        Task<ServiceResponse<List<GetUserDto>>> Delete(int id);
    }
}
