using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace PV221Chat.SignalR
{
    public class GlobalChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            // Все пользователи будут подключаться к группе "GlobalChat"
            await Groups.AddToGroupAsync(Context.ConnectionId, "GlobalChat");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Отключаем пользователя от группы "GlobalChat"
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "GlobalChat");
            await base.OnDisconnectedAsync(exception);
        }

    }
}
