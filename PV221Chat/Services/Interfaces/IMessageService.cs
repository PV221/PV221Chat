using PV221Chat.Core.DataModels;
using PV221Chat.DTO;

namespace PV221Chat.Services.Interfaces
{
    public interface IMessageService
    {
        Task SendNewMessageAsync(int chatId, MessageDTO message);
        Task SendNewMessageToGlobalMessageAsync(GlobalChatMessageDTO message);
    }
}
