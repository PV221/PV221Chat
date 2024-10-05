using Microsoft.AspNetCore.SignalR;

namespace PV221Chat.SignalR
{
    public class ChatHub : Hub
    {
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
