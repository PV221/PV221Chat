using Microsoft.EntityFrameworkCore;
using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories.EF;

namespace PV221Chat.Core.Repositories;

public class UserChatRepository : EFRepository<UserChat>, IUserChatRepository
{
    public UserChatRepository(Pv221chatContext context) : base(context)
    {
    }

    public async Task<UserChat?> FindUserChatByUserIdAndChatIdAsync(int userId, int chatId)
    {
        return await _context.UserChats
           .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChatId == chatId);
    }

    public async Task<IEnumerable<UserChat>> GetAllUserChatsAsync(int chatId)
    { 
        var userChats = await _context.UserChats
                         .Where(uc => uc.ChatId == chatId)
                         .ToListAsync();
        return userChats;
    }
}
