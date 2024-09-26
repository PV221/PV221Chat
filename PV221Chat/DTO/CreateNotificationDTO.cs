using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class CreateNotificationDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string NotificationText { get; set; }
    }

}
