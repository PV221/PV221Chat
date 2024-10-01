using System.Collections.Generic;
using PV221Chat.DTO;

namespace PV221Chat.Models
{
    public class BlogPostViewModel
    {
        public UserDTO User { get; set; }
        public UpdateBlogPageDTO BlogPost { get; set; }
        public UpdateBlogPageDTO UpdateBlogPost { get; set; }
        public List<BlogPageDTO> BlogPosts { get; set; }

        public BlogPostViewModel()
        {
            BlogPosts = new List<BlogPageDTO>();
        }
    }
}
