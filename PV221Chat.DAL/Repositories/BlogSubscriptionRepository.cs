using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.DAL.Interfaces;

namespace PV221Chat.DAL.Repositories;

public class BlogSubscriptionRepository : DataRepository<BlogSubscription>, IBlogSubscriptionRepository
{
    public BlogSubscriptionRepository(Pv221chatContext context) : base(context)
    {
    }
}
