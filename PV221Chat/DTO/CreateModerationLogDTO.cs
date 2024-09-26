using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class CreateModerationLogDTO
    {
        [Required]
        public int ModeratorId { get; set; }

        [Required]
        public int TargetUserId { get; set; }

        public int? TargetChatId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Action { get; set; }
    }

}
