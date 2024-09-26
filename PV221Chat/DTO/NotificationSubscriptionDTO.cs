namespace PV221Chat.DTO
{
    public class NotificationSubscriptionDTO
    {
        public int SubscriptionId { get; set; }
        public int? UserId { get; set; }
        public int? BlogId { get; set; }
        public DateTime? SubscribedAt { get; set; }
    }

}
