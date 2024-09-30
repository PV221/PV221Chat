using PV221Chat.Core.DataModels;

namespace PV221Chat.Core.Interfaces;

public interface INotificationRepository : IDataRepository<Notification>
{
    Task<IEnumerable<Notification>> GetUnreadNotificationsByUserAndChatIdAsync(int chatId, int userId);
    Task<List<UserChat>> GetUsersInChatExceptSenderAsync(int chatId, int senderId);
}
