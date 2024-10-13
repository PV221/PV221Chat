using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.DAL.Interfaces;

namespace PV221Chat.DAL.Repositories;

public class GlobalChatMessageRepository : DataRepository<GlobalChatMessage>, IGlobalChatMessageRepository
{
    public GlobalChatMessageRepository(Pv221chatContext context) : base(context)
    {
    }

    public async Task<GlobalChatMessage> AddDataReturnedMessageAsync(GlobalChatMessage message)
    {
        await _context.GlobalChatMessages.AddAsync(message);
        await _context.SaveChangesAsync();

        return message;
    }

}
