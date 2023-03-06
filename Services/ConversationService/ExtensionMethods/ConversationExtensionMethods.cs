using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Dtos.Conversation;

namespace SupportTicketSystem.Services.ConversationService.ExtensionMethods
{
    public static class JoinUserTicketExtensionMethods
    {
            public static async Task<List<GetConversationDto>> GetConversationDtoFromQuery(this IQueryable<Conversation> query, IMapper mapper)
            {
                return await query.Select(c => mapper.Map<GetConversationDto>(c)).ToListAsync();
            }

            public static async Task<Conversation> GetById(this IQueryable<Conversation> query, int id)
            {
                return await query.FirstOrDefaultAsync(x => x.Id == id);
            }
    }
}
