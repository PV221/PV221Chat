using Microsoft.EntityFrameworkCore; 
using PV221Chat.Core.DataContext;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories.EF;

namespace PV221Chat.Core.Repositories
{
    public class BlogPageRepository : EFRepository<BlogPage>, IBlogPageRepository
    {
        private readonly Pv221chatContext _context;

        public BlogPageRepository(Pv221chatContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<BlogPage> GetByIdAsync(int blogId)
        {
            return await _context.BlogPages.FindAsync(blogId);
        }

        public async Task<IEnumerable<BlogPage>> GetListDataAsync()
        {
            return await _context.BlogPages.ToListAsync();
        }

        public async Task AddDataAsync(BlogPage blogPage)
        {
            await _context.BlogPages.AddAsync(blogPage);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Blog post added: {blogPage.Title}, Content: {blogPage.Content}");
        }

        public async Task UpdateDataAsync(BlogPage blogPage)
        {
            _context.BlogPages.Update(blogPage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDataAsync(int blogId)
        {
            var blogPage = await GetByIdAsync(blogId);
            if (blogPage != null)
            {
                _context.BlogPages.Remove(blogPage);
                await _context.SaveChangesAsync();
            }
        }
    }
}
