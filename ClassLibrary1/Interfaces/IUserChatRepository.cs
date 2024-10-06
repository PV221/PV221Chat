using PV221Chat.Core.DataModels;

namespace PV221Chat.Core.Interfaces;

public interface IUserChatRepository : IDataRepository<UserChat>
{
    Task<UserChat?> FindUserChatByUserIdAndChatIdAsync(int userId, int chatId);
    Task<IEnumerable<UserChat>> GetAllUserChatsAsync(int chatId);
}
