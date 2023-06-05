using SupportTicketSystem.Dtos.Conversation;

namespace SupportTicketSystem.Services.ConversationService
{
    public interface IConversationService
    {
        /// <summary>
        /// An async task that gets all conversations from DB.
        /// </summary>
        /// <returns> A list of Conversation. </returns>
        Task<ServiceResponse<List<GetConversationDto>>> GetAll();
        /// <summary>
        /// An async task that gets the conversation that is specified by id from the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The conversation that is specified by id. </returns>
        Task<ServiceResponse<GetConversationDto>> GetById(int id);
        /// <summary>
        /// An async task that adds a conversation to the DB.
        /// </summary>
        /// <param name="newConversation"></param>
        /// <returns> A list of conversations. </returns>
        Task<ServiceResponse<List<GetConversationDto>>> Add(AddConversationDto newConversation);
        /// <summary>
        /// An async task that updates the conversation that is specified by id to the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateConversation"></param>
        /// <returns> The conversation that is specified by id. </returns>
        Task<ServiceResponse<GetConversationDto>> Update(int id, UpdateConversationDto updateConversation);
        /// <summary>
        /// An async task that archieves the conversation with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A list of conversations. </returns>
        Task<ServiceResponse<List<GetConversationDto>>> Delete(int id);
        /// <summary>
        /// AN async task that updates the log of the conversation that is specified with id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateLog"></param>
        /// <returns> The conversation that is specified by id. </returns>
        Task<ServiceResponse<GetConversationDto>> UpdateLog(int id, string updateLog);
    }
}
