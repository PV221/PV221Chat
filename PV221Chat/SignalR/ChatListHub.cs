using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace PV221Chat.SignalR;

public class ChatListHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var chatIdsQueryString = Context.GetHttpContext().Request.Query["chatIds"].ToString();

        if (string.IsNullOrEmpty(chatIdsQueryString))
        {
            return;
        }

        var chatIds = chatIdsQueryString.Split(',');

        foreach (var chatId in chatIds)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var chatIdsQueryString = Context.GetHttpContext().Request.Query["chatIds"].ToString();

        if (!string.IsNullOrEmpty(chatIdsQueryString))
        {
            var chatIds = chatIdsQueryString.Split(',');

            foreach (var chatId in chatIds)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }
}
