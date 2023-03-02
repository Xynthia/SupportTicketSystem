﻿using AutoMapper;
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


        }
    }
}
