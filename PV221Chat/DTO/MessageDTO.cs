namespace PV221Chat.DTO
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public int? ChatId { get; set; }
        public int? SenderId { get; set; }
        public string SenderName { get; set; } = null!;
        public string MessageText { get; set; } = null!;
        public DateTime? SentAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
