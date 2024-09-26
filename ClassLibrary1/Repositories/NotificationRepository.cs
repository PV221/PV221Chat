using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories.EF;

namespace PV221Chat.Core.Repositories;

public class NotificationRepository : EFRepository<Notification>, INotificationRepository
{
    public NotificationRepository(Pv221chatContext context) : base(context)
    {
    }
}
