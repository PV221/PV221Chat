using Microsoft.EntityFrameworkCore;
using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories.EF;

namespace PV221Chat.Core.Repositories;

public class NotificationRepository : EFRepository<Notification>, INotificationRepository
{
    public NotificationRepository(Pv221chatContext context) : base(context)
    {
    }
    public async Task<IEnumerable<Notification>> GetUnreadNotificationsByUserAndChatIdAsync(int chatId, int userId)
    {
        return await _context.Notifications
            .Where(n => n.UserChat.ChatId == chatId && n.UserChat.UserId == userId && n.IsRead == false)
            .ToListAsync();
    }

    public async Task<List<UserChat>> GetUsersInChatExceptSenderAsync(int chatId, int senderId)
    {
        return await _context.UserChats
            .Where(uc => uc.ChatId == chatId && uc.UserId != senderId)
            .Include(uc => uc.User)
            .ToListAsync();
    }

    public async Task ReadNotifiByUserChatId(int userChatId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.UserChatId == userChatId && n.IsRead == false)
            .ToListAsync();

        if (notifications != null && notifications.Count > 0)
        {
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
