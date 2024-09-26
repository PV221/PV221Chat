using System.ComponentModel.DataAnnotations;
namespace PV221Chat.DTO
{
    public class CreateUserRatingDTO
    {
        [Required]
        public int RatedUserId { get; set; }

        [Required]
        public int RatedById { get; set; }

        [Range(1, 5)]
        public int RatingScore { get; set; }

        [MaxLength(255)]
        public string? RatingComment { get; set; }
    }

}
