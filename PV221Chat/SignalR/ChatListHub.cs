using Microsoft.AspNetCore.SignalR;

namespace PV221Chat.SignalR;

public class ChatListHub : Hub
{
    public async Task SendNotification(string chatId, string message)
    {
        await Clients.Group(chatId).SendAsync("ReceiveNotification", message);
    }
    public override async Task OnConnectedAsync()
    {
        var chatId = Context.GetHttpContext().Request.Query["chatId"];
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var chatId = Context.GetHttpContext().Request.Query["chatId"];
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        await base.OnDisconnectedAsync(exception);
    }
}
