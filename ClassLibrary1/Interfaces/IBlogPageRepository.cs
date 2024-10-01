using System.Collections.Generic;
using System.Threading.Tasks;
using PV221Chat.Core.DataModels;

namespace PV221Chat.Core.Interfaces
{
    public interface IBlogPageRepository
    {
        Task<BlogPage> GetByIdAsync(int blogId);
        Task<IEnumerable<BlogPage>> GetListDataAsync();
        Task AddDataAsync(BlogPage blogPage);
        Task UpdateDataAsync(BlogPage blogPage);
        Task DeleteDataAsync(int blogId);
    }
}
