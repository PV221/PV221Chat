using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.DAL.Interfaces;

namespace PV221Chat.DAL.Repositories;

public class ModerationLogRepository : DataRepository<ModerationLog>, IModerationLogRepository
{
    public ModerationLogRepository(Pv221chatContext context) : base(context)
    {
    }
}
