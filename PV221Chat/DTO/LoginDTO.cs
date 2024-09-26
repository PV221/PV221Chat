using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

}
