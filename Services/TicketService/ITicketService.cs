﻿using SupportTicketSystem.Dtos.Ticket;

namespace SupportTicketSystem.Services.TicketService
{
    public interface ITicketService
    {
        Task<ServiceResponse<List<GetTicketDto>>> GetAll();
        Task<ServiceResponse<GetTicketDto>> GetById(int id);
        Task<ServiceResponse<List<GetTicketDto>>> Add(AddTicketDto newTicket);
        Task<ServiceResponse<GetTicketDto>> Update(int id, UpdateTicketDto updateTicket);
        Task<ServiceResponse<List<GetTicketDto>>> Delete(int id);
        Task<ServiceResponse<GetTicketDto>> UpdateStatus(int id, UpdateTicketDto updateTicket);
    }
}