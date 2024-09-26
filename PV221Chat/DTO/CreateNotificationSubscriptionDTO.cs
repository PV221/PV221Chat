using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class CreateNotificationSubscriptionDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BlogId { get; set; }
    }

}
