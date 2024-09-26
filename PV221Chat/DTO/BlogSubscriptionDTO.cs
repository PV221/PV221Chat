namespace PV221Chat.DTO
{
    public class BlogSubscriptionDTO
    {
        public int SubscriptionId { get; set; }
        public int? BlogId { get; set; }
        public int? UserId { get; set; }
        public DateTime? SubscribedAt { get; set; }
    }

}
