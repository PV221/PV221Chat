using Microsoft.AspNetCore.SignalR;
using PV221Chat.Core.DataModels;
using PV221Chat.DTO;
using PV221Chat.Services.Interfaces;
using PV221Chat.SignalR;

namespace PV221Chat.Services
{
    public class MessageService : IMessageService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNewMessageAsync(int chatId, MessageDTO message)
        {
            await _hubContext.Clients.Group(chatId.ToString())
                .SendAsync("ReceiveMessage", chatId, message);
        }
        public async Task SendNewMessageToGlobalMessageAsync(GlobalChatMessageDTO message)
        {
            await _hubContext.Clients.Group("GlobalChat")
                .SendAsync("ReceiveMessage", message);
        }
    }
}
