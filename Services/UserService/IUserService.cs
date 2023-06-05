using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Dtos.Ticket;
using SupportTicketSystem.Dtos.UserDtos;

namespace SupportTicketSystem.Services.UserService
{
    public interface IUserService
    {
        /// <summary>
        /// An async task that gets all the user from the DB.
        /// </summary>
        /// <returns> A list of Users. </returns>
        Task<ServiceResponse<List<GetUserDto>>> GetAll();
        /// <summary>
        /// An async task that gets the ticket with the specified id from DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A ticket has the specified id. </returns>
        Task<ServiceResponse<GetUserDto>> GetById(int id);
        /// <summary>
        /// An async task that adds an User to the DB.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns> A list of Users. </returns>
        Task<ServiceResponse<List<GetUserDto>>> Add(AddUserDto newUser);
        /// <summary>
        /// An async task that updates an User with the specified id to the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUser"></param>
        /// <returns> The User that has the specified id. </returns>
        Task<ServiceResponse<GetUserDto>> Update(int id, UpdateUserDto updateUser);
        /// <summary>
        /// An async task that archieves the user with the specified id to the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The User that has the specified id. </returns>
        Task<ServiceResponse<List<GetUserDto>>> Delete(int id);
        /// <summary>
        /// An async task that gets all the tickets that the User, specified by id, has made.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A list of tickets. </returns>
        Task<ServiceResponse<List<GetTicketDto>>> GetAllCreatedTickets(int id);
        /// <summary>
        /// An async task that gets all the tickets that the User, specified by id, got involved in. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A list of tickets. </returns>
        Task<ServiceResponse<List<GetJoinUserTicketDto>>> GetAllInvolvedTickets(int id);
        /// <summary>
        /// An async task that updates the User, specified by id, if they are or are not a secretview user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="secretview"></param>
        /// <returns> The User that was specified by id. </returns>
        Task<ServiceResponse<GetUserDto>> UpdateSecretView(int id, bool secretview);
        
    }
}
