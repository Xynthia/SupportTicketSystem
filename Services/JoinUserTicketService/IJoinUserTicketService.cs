using SupportTicketSystem.Dtos.JoinUserTicket;

namespace SupportTicketSystem.Services.JoinUserTicketService
{
    public interface IJoinUserTicketService
    {
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> GetAll();
        Task<ServiceResponse<GetJoinUserTicketDto>> GetById(int id);
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> Add(AddJoinUserTicketDto newJoinUserTicket);
        Task<ServiceResponse<GetJoinUserTicketDto>> Update(int id, UpdateJoinUserTicketDto updateJoinUserTicket);
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> Delete(int id);
    }
}
