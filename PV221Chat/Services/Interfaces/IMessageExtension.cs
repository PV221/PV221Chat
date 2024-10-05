using PV221Chat.DTO;

namespace PV221Chat.Services.Interfaces
{
    public interface IMessageExtension
    {
        Task<MessageDTO> SendMessageAsync(int chatId, string messageText, int senderId);
    }
}
