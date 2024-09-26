using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class CreateChatDTO
    {
        [MaxLength(100)]
        public string? ChatName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ChatType { get; set; }

        [Required]
        public bool IsOpen { get; set; }
    }
}
