using PV221Chat.Core.DataModels;

namespace PV221Chat.DAL.Interfaces
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
