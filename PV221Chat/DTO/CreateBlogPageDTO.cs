using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class CreateBlogPageDTO
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string? Content { get; set; }

        [Required]
        [MaxLength(255)]
        public string Type { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }

}
