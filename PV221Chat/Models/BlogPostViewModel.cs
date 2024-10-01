using PV221Chat.DTO;

namespace PV221Chat.Models
{
    public class BlogPostViewModel
    {
        public UserDTO User { get; set; }
        public UpdateBlogPageDTO UpdateBlogPost { get; set; }
        public IEnumerable<BlogPageDTO> BlogPosts { get; set; }
    }
}
