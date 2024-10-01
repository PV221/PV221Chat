using System.ComponentModel.DataAnnotations;

namespace PV221Chat.DTO
{
    public class UpdateBlogPageDTO
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public static implicit operator UpdateBlogPageDTO(BlogPageDTO blogPageDTO)
        {
            if (blogPageDTO == null)
            {
                return null; 
            }

            return new UpdateBlogPageDTO
            {
                BlogId = blogPageDTO.BlogId,
                Title = blogPageDTO.Title,
                Content = blogPageDTO.Content ?? string.Empty,
                Type = blogPageDTO.Type,
                CreatedAt = blogPageDTO.CreatedAt ?? DateTime.Now 
            };
        }
    }
}
