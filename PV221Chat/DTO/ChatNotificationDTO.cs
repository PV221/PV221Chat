namespace PV221Chat.DTO
{
    public class ChatNotificationDTO
    {
        public int NotificationId { get; set; }
        public int ChatId { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }

}
