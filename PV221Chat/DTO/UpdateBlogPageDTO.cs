using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class UpdateBlogPageDTO
    {
        [MaxLength(255)]
        public string? Title { get; set; }

        public string? Content { get; set; }

        [MaxLength(255)]
        public string? Type { get; set; }
    }

}
