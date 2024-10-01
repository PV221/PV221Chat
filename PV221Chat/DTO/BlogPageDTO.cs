using System.ComponentModel.DataAnnotations;

namespace PV221Chat.DTO
{
    public class BlogPageDTO
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public int? AuthorId { get; set; }
        public string Type { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
