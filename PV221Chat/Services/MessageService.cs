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
        private readonly IHubContext<GlobalChatHub> _hubGlobalChatContext;

        public MessageService(IHubContext<ChatHub> hubContext, IHubContext<GlobalChatHub> hubGlobalChatContext)
        {
            _hubContext = hubContext;
            _hubGlobalChatContext = hubGlobalChatContext;
        }

        public async Task SendNewMessageAsync(int chatId, MessageDTO message)
        {
            await _hubContext.Clients.Group(chatId.ToString())
                .SendAsync("ReceiveMessage", chatId, message);
        }
        public async Task SendNewMessageToGlobalMessageAsync(GlobalChatMessageDTO message)
        {
            await _hubGlobalChatContext.Clients.Group("GlobalChat")
                .SendAsync("ReceiveMessage", message);
        }
    }
}
