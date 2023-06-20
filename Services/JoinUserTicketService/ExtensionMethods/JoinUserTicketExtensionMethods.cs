using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Dtos.JoinUserTicket;

namespace SupportTicketSystem.Services.JoinUserTicketService.ExtensionMethods
{
    public static class JoinUserTicketExtensionMethods
    {
        public static async Task<List<GetJoinUserTicketDto>> GetJoinTicketDtoFromQuery(this IQueryable<JoinUserTicket> query, MapperlyProfile mapper)
        {
            return await query.Select(c => mapper.JoinUserTicketToGetJoinUserTicketDto(c)).ToListAsync();
        }

        public static async Task<JoinUserTicket> GetById(this IQueryable<JoinUserTicket> query, int id)
        {
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

         public static async Task<List<JoinUserTicket>> GetByUserId(this IQueryable<JoinUserTicket> query, int id)
         {
            return await query.Where(t => id == t.UserId).ToListAsync();
         }

    }
}
