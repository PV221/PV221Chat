using PV221Chat.Core.DataModels;

namespace PV221Chat.Core.Interfaces;

public interface IMessageRepository : IDataRepository<Message>
{
    Task<List<Message>> GetByChatIdAndSorted(int chatId, int limit);
}
