using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace PV221Chat.SignalR
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var chatId = Context.GetHttpContext().Request.Query["chatId"];

            if (chatId.IsNullOrEmpty())
            {
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var chatId = Context.GetHttpContext().Request.Query["chatId"];

            if (chatId.IsNullOrEmpty())
            {
                return;
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
            await base.OnDisconnectedAsync(exception);
        }

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
        //public async Task Ping()
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", "System", "Ping test message");
        //}
    }
}
