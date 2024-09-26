namespace PV221Chat.DTO
{
    public class NotificationDTO
    {
        public int NotificationId { get; set; }
        public int? UserId { get; set; }
        public string? NotificationText { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsRead { get; set; }
    }
}
