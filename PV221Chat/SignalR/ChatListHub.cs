using Microsoft.AspNetCore.SignalR;

namespace PV221Chat.SignalR;

public class ChatListHub : Hub
{
    //public override async Task OnConnectedAsync()
    //{
    //    var chatId = Context.GetHttpContext().Request.Query["chatId"];
    //    await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    //    await base.OnConnectedAsync();
    //}

    //public override async Task OnDisconnectedAsync(Exception exception)
    //{
    //    var chatId = Context.GetHttpContext().Request.Query["chatId"];
    //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    //    await base.OnDisconnectedAsync(exception);
    //}

    //public async Task SendNotificationToGroup(int chatId, string message, int unreadCount)
    //{
    //    await Clients.Group(chatId.ToString())
    //        .SendAsync("ReceiveNotification", chatId, message, unreadCount);
    //}
}
