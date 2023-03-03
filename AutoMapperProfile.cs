using AutoMapper;
using SupportTicketSystem.Dtos.Conversation;
using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Dtos.Ticket;
using SupportTicketSystem.Dtos.UserDtos;

namespace SupportTicketSystem
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            CreateMap<Ticket, GetTicketDto>();
            CreateMap<AddTicketDto, Ticket>();
            CreateMap<UpdateTicketDto, Ticket>();

            CreateMap<Conversation, GetConversationDto>();
            CreateMap<AddConversationDto, Conversation>();
            CreateMap<UpdateConversationDto, Conversation>();

            CreateMap<JoinUserTicket, GetJoinUserTicketDto>();
            CreateMap<AddJoinUserTicketDto, JoinUserTicket>();
            CreateMap<UpdateJoinUserTicketDto, JoinUserTicket>();
        }
    }
}
