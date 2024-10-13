using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.DAL.Interfaces;

namespace PV221Chat.DAL.Repositories;

public class UserRatingRepository : DataRepository<UserRating>, IUserRatingRepository
{
    public UserRatingRepository(Pv221chatContext context) : base(context)
    {
    }
}
