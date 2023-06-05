using SupportTicketSystem.Dtos.JoinUserTicket;

namespace SupportTicketSystem.Services.JoinUserTicketService
{
    public interface IJoinUserTicketService
    {
        /// <summary>
        /// an async task that gets all the joinUserTickets from the DB.
        /// </summary>
        /// <returns> A list of all JoinUserTickets. </returns>
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> GetAll();
        /// <summary>
        /// An async task that gets the JoinUserTicket with the specified id from DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A JoinUserTicket with specified id. </returns>
        Task<ServiceResponse<GetJoinUserTicketDto>> GetById(int id);
        /// <summary>
        /// An async task that adds a JoinUserTicket to the DB.
        /// </summary>
        /// <param name="newJoinUserTicket"></param>
        /// <returns> A list of all JoinUserTickets. </returns>
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> Add(AddJoinUserTicketDto newJoinUserTicket);
        /// <summary>
        /// An async task that updates the specified joinUserTicket to the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateJoinUserTicket"></param>
        /// <returns> The updated joinUserTicket. </returns>
        Task<ServiceResponse<GetJoinUserTicketDto>> Update(int id, UpdateJoinUserTicketDto updateJoinUserTicket);
        /// <summary>
        /// An async task that archieves the specified JoinUserTicket from DB..
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A list of all JoinUserTickets. </returns>
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> Delete(int id);
    }
}
