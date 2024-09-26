using Microsoft.EntityFrameworkCore;
using PV221Chat.Core.DataContext;
using PV221Chat.Core.Interfaces;

namespace PV221Chat.Core.Repositories.EF;

public class EFRepository<T> : IDataRepository<T> where T : class
{
    protected readonly Pv221chatContext _context;
    public EFRepository(Pv221chatContext context)
    {
        _context = context;
    }
    public async Task<bool> AddDataAsync(T data)
    {
        try
        {
            _context.Set<T>().Add(data);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateDataAsync(int? id, T data)
    {
        try
        {
            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteDataAsync(int id)
    {
        try
        {
            var D = await GetDataAsync(id);
            _context.Set<T>().Remove(D);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public virtual async Task<T?> GetDataAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetListDataAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
}
