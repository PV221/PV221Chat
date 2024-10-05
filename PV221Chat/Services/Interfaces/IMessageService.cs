using PV221Chat.DTO;

namespace PV221Chat.Services.Interfaces
{
    public interface IMessageService
    {
        Task SendNewMessageAsync(int chatId, MessageDTO message);
    }
}
