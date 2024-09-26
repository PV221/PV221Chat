using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class CreateBlogSubscriptionDTO
    {
        [Required]
        public int BlogId { get; set; }

        [Required]
        public int UserId { get; set; }
    }

}
