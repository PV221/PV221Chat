namespace PV221Chat.DTO
{
    public class UserRatingDTO
    {
        public int RatingId { get; set; }
        public int? RatedUserId { get; set; }
        public int? RatedById { get; set; }
        public int? RatingScore { get; set; }
        public string? RatingComment { get; set; }
        public DateTime? RatedAt { get; set; }
    }

}
