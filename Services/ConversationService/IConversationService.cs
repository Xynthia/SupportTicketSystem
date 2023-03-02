using SupportTicketSystem.Dtos.Conversation;

namespace SupportTicketSystem.Services.ConversationService
{
    public interface IConversationService
    {
        Task<ServiceResponse<List<GetConversationDto>>> GetAll();
        Task<ServiceResponse<GetConversationDto>> GetById(int id);
        Task<ServiceResponse<List<GetConversationDto>>> Add(AddConversationDto newConversation);
        Task<ServiceResponse<GetConversationDto>> Update(int id, UpdateConversationDto updateConversation);
        Task<ServiceResponse<List<GetConversationDto>>> Delete(int id);
    }
}
