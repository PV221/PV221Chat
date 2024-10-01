using PV221Chat.Core.DataModels;

namespace PV221Chat.Core.Interfaces;

public interface IUserRepository : IDataRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
    Task<bool> IsPresentAsync(string email);
    Task<bool> IsValidUserAsync(string email, string password);
    Task<IEnumerable<User>> FindByChatIdAsync(int chatId);
    Task<Chat?> GetPrivateChatBetweenUsersAsync(int userId1, int userId2);
}
