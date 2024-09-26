using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class CreateMessageDTO
    {
        [Required]
        public int ChatId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public string MessageText { get; set; }
    }

}
