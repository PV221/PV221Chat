using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories.EF;

namespace PV221Chat.Core.Repositories;

public class UserRepository : EFRepository<User>, IUserRepository
{
    public UserRepository(Pv221chatContext context) : base(context)
    {
    }

    public async Task<bool> IsPresent(string email)
    {
        return await _context.Users
             .AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsValidUser(string email, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return false;

        if (user.PasswordHash == password)
        {
            return true;
        }

        return false;
    }
}
