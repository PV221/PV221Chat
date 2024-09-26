using PV221Chat.Core.DataModels;

namespace PV221Chat.Core.Interfaces;

public interface IUserRepository : IDataRepository<User>
{
    Task<bool> IsPresent(string email);
    Task<bool> IsValidUser(string email, string password);
}
