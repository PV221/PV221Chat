namespace PV221Chat.Services.Interfaces
{
    public interface IMessageService
    {
        Task SendMessageAsync(int chatId, string messageText, int senderId);
    }
}
