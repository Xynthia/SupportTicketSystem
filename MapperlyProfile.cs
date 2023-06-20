using Riok.Mapperly.Abstractions;
using SupportTicketSystem.Dtos.Conversation;
using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Dtos.Ticket;
using SupportTicketSystem.Dtos.UserDtos;

namespace SupportTicketSystem
{
    [Mapper]
    public partial class MapperlyProfile
    {
        public partial Ticket AddTicketDtoToTicket(AddTicketDto newTicket);
        public partial Ticket UpdateTicketDtoToTicket(UpdateTicketDto updateTicket);
        public partial GetTicketDto TicketToGetTicketDto(Ticket ticket);
        public partial List<GetTicketDto> TicketsToGetTicketDto(List<Ticket> tickets);

        public partial JoinUserTicket AddJoinUserTicketDtoToJoinUserTicket(AddJoinUserTicketDto newJoinUserTicket);
        public partial JoinUserTicket UpdateJoinUserTicketDtoToJoinTicket(UpdateJoinUserTicketDto newJoinUserTicket);
        public partial GetJoinUserTicketDto JoinUserTicketToGetJoinUserTicketDto(JoinUserTicket joinUserTicket);
        public partial List<GetJoinUserTicketDto> JoinUserTicketsToGetJoinUserTicketDto(List<JoinUserTicket> joinUserTickets);

        public partial User AddUserDtoToUser(AddUserDto newUser);
        public partial User UpdateUserDtoToUser(UpdateUserDto newUser);
        public partial GetUserDto UserToGetUserDto(User user);

        public partial Conversation AddConversationDtoToConversation(AddConversationDto newConversation);
        public partial Conversation UpdateConversationDtoToConversation(UpdateConversationDto newConversation);
        public partial GetConversationDto ConversationToGetConversationDto(Conversation conversation);
    }
}
