using Microsoft.AspNetCore.SignalR;
using PV221Chat.Mapper;
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

        //// Метод отправки сообщения
        //public async Task SendMessage(string messageText)
        //{
        //    var message = new
        //    {
        //        MessageId = Guid.NewGuid(),  // Уникальный идентификатор
        //        SenderId = Context.ConnectionId,  // ID отправителя
        //        MessageText = messageText,
        //        SentAt = DateTime.Now  // Время отправки
        //    };

        //    // Отправляем сообщение всем пользователям в группе "GlobalChat"
        //    await Clients.Group("GlobalChat").SendAsync("ReceiveMessage", message);
        //}
    }
}
