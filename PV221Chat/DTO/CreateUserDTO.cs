using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
   
    public class CreateUserDTO
    {
        [Required]
        [MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

}
