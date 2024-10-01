using PV221Chat.Core.DataModels;

namespace PV221Chat.Core.Interfaces;

public interface IChatRepository : IDataRepository<Chat>
{
    Task<IEnumerable<Chat>> GetDataByUserIdAsync(int userId);
    Task<IEnumerable<User>> GetUsersByChatId(int chatId);
    Task<Chat> CreatePrivateChatAsync(int userId1, int userId2);
}
