using PV221Chat.Core.DataModels;
using PV221Chat.DTO;

namespace PV221Chat.Services.Interfaces
{
    public interface IMessageExtension
    {
        Task<MessageDTO> SendMessageAsync(int chatId, string messageText, int senderId);
        Task<GlobalChatMessageDTO> SendMessageToGlobalChatAsync(string messageText, int senderId);
    }
}
