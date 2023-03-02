using SupportTicketSystem.Dtos.JoinUserTicket;

namespace SupportTicketSystem.Services.JoinUserTicketService
{
    public interface IJoinUserTicketService
    {
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> GetAll();
        Task<ServiceResponse<GetJoinUserTicketDto>> GetById(int id);
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> Add(AddJoinUserTicketDto newConversation);
        Task<ServiceResponse<GetJoinUserTicketDto>> Update(int id, UpdateJoinUserTicketDto updateConversation);
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> Delete(int id);
    }
}
