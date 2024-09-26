using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories.EF;

namespace PV221Chat.Core.Repositories;

public class UserChatRepository : EFRepository<UserChat>, IUserChatRepository
{
    public UserChatRepository(Pv221chatContext context) : base(context)
    {
    }
}
