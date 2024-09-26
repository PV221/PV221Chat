using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class UpdateChatDTO
    {
        [MaxLength(100)]
        public string? ChatName { get; set; }

        [MaxLength(50)]
        public string? ChatType { get; set; }

        public bool? IsOpen { get; set; }
    }

}
