namespace PV221Chat.DTO
{
    public class BlogPostDTO
    {
        public int BlogPostId { get; set; }

        public int BlogPageId { get; set; }

        public string? Title { get; set; }

        public string Text { get; set; } = null!;

        public DateTime? CreateAt { get; set; }
    }
}
