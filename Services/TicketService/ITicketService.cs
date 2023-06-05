using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Dtos.Ticket;

namespace SupportTicketSystem.Services.TicketService
{
    public interface ITicketService
    {
        /// <summary>
        /// An async task that gets all the Tickets from the DB.
        /// </summary>
        /// <returns> A list of Tickets. </returns>
        Task<ServiceResponse<List<GetTicketDto>>> GetAll();
        /// <summary>
        /// An async task that gets the ticket with the specified id from DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A ticket that has the specified id. </returns>
        Task<ServiceResponse<GetTicketDto>> GetById(int id);
        /// <summary>
        /// An async task that adds a ticket to the DB.
        /// </summary>
        /// <param name="newTicket"></param>
        /// <returns>  A list of Tickets </returns>
        Task<ServiceResponse<List<GetTicketDto>>> Add(AddTicketDto newTicket);
        /// <summary>
        /// An async task that updates the ticket with the specified id to the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateTicket"></param>
        /// <returns>  The specified ticket </returns>
        Task<ServiceResponse<GetTicketDto>> Update(int id, UpdateTicketDto updateTicket);
        /// <summary>
        /// An async task that archieves the ticket with the specified id to the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>  A list of Tickets </returns>
        Task<ServiceResponse<List<GetTicketDto>>> Delete(int id);
        /// <summary>
        /// An async task that updates the status from a ticket specified by id to the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateTicket"></param>
        /// <returns> The specified ticket </returns>
        Task<ServiceResponse<GetTicketDto>> UpdateStatus(int id, UpdateTicketDto updateTicket);
        /// <summary>
        /// An aasync task that adds a UserInvolved to the DB. Also sends a message to user of the ticket explaining their ticket changed.
        /// </summary>
        /// <param name="newJoinUserTicket"></param>
        /// <returns> The specified JoinUserTicket </returns>
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> AddUsersInvolved(AddJoinUserTicketDto newJoinUserTicket);
        /// <summary>
        /// An async task that updates the severity level of a ticket 1 level up.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The Specified ticket </returns>
        Task<ServiceResponse<GetTicketDto>> UpdateSeverityLevelUp(int id);
        /// <summary>
        /// An async task that updates the severity level of a ticket 1 level down.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The Specified ticket </returns>
        Task<ServiceResponse<GetTicketDto>> UpdateSeverityLevelDown(int id);
        /// <summary>
        /// An async task that updates the person responsible to the specified ticket .
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns> The Specified ticket </returns>
        Task<ServiceResponse<GetTicketDto>> UpdateResposible(int id, int userId);
        /// <summary>
        /// An async task that get the user that has the least amount of tickets they are reseponsible for.
        /// </summary>
        /// <returns> The user Id. </returns>
        Task<ServiceResponse<int>> GetLeastAmountResposibleFor();

    }
}
