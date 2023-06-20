using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Dtos.Ticket;

namespace SupportTicketSystem.Services.TicketService.ExtensionMethods
{
    public static class TicketExtensionMethods
    {
            public static async Task<List<GetTicketDto>> GetTicketDtoFromQuery(this IQueryable<Ticket> query, MapperlyProfile mapper)
            {
                return await query.Select(c => mapper.TicketToGetTicketDto(c)).ToListAsync();
            }

            public static async Task<Ticket> GetById(this IQueryable<Ticket> query, int id)
            {
                return await query.FirstOrDefaultAsync(x => x.Id == id);
            }

            public static async Task<List<Ticket>> GetByCreatedId(this IQueryable<Ticket> query, int id)
            {
                return await query.Where(t => id == t.CreatedByID).ToListAsync();
            }
    }
}
