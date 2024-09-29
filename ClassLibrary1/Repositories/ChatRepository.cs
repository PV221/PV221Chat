using Microsoft.EntityFrameworkCore;
using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories.EF;

namespace PV221Chat.Core.Repositories;

public class ChatRepository : EFRepository<Chat>, IChatRepository
{
    private readonly Pv221chatContext _context;
    public ChatRepository(Pv221chatContext context) : base(context)
    {
        _context = context;
    }
    public Chat GetChatById(int id)
    {
        return _context.Chats.FirstOrDefault(c => c.ChatId == id);
    }

    public async Task<IEnumerable<Chat>> GetDataByUserIdAsync(int userId)
    {
        var userChats = await _context.UserChats
           .Where(uc => uc.UserId == userId)
           .Include(uc => uc.Chat)
           .Select(uc => uc.Chat) 
           .ToListAsync();

        return userChats;
    }
}
