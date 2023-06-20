using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Dtos.UserDtos;

namespace SupportTicketSystem.Services.UserService.ExtensionMethods
{
    public static class UserExtensionMethods
    {
            public static async Task<List<GetUserDto>> GetUserDtoFromQuery(this IQueryable<User> query, MapperlyProfile mapper)
            {
                return await query.Select(c => mapper.UserToGetUserDto(c)).ToListAsync();
            }

            public static async Task<User> GetById(this IQueryable<User> query, int id)
            {
                return await query.FirstOrDefaultAsync(x => x.Id == id);
            }
    }
}
