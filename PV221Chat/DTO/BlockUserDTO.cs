using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class BlockUserDTO
    {
        [Required]
        public int BlockedByUserId { get; set; }

        [Required]
        public int BlockedUserId { get; set; }

        public int? ChatId { get; set; }
    }

}
