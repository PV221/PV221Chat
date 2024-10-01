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

    public async Task<Chat> CreatePrivateChatAsync(int userId1, int userId2)
    {
        var chat = new Chat
        {
            ChatName = null, 
            ChatType = "Private",
            IsOpen = false,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync(); 

        await _context.UserChats.AddAsync(new UserChat { UserId = userId1, ChatId = chat.ChatId });
        await _context.UserChats.AddAsync(new UserChat { UserId = userId2, ChatId = chat.ChatId });

        await _context.SaveChangesAsync(); 

        return chat; 
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
