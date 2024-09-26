using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class UpdateUserDTO
    {
        [MaxLength(50)]
        public string? Nickname { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [MinLength(6)]
        public string? Password { get; set; }

        public string? AvatarUrl { get; set; }

        public string? Hobbies { get; set; }

        public string? Skills { get; set; }

        public DateOnly? BirthDate { get; set; }
    }
}
