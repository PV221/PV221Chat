using Microsoft.AspNetCore.SignalR;
using PV221Chat.Services.Interfaces;
using PV221Chat.SignalR;

namespace PV221Chat.Core.Services.WithHub;

public class NotificationService : INotificationService
{
    private readonly IHubContext<ChatListHub> _hubContext;

    public NotificationService(IHubContext<ChatListHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNewNotificationAsync(int chatId, string message)
    {
        await _hubContext.Clients.Group(chatId.ToString())
            .SendAsync("ReceiveNotification", chatId, message);
    }
}
