using PV221Chat.Core.DataModels;

namespace PV221Chat.Core.Interfaces;

public interface IMessageRepository : IDataRepository<Message>
{
    Task<List<Message>> GetByChatIdAndSortedAsync(int chatId, int limit);
    Task<Message> AddDataReturnedMessageAsync(Message message);
}
