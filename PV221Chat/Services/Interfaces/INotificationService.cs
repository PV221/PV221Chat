namespace PV221Chat.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendNewNotificationAsync(int chatId, string message, int unreadCount);
    }
}
