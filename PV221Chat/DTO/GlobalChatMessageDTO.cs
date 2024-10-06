namespace PV221Chat.DTO
{
    public class GlobalChatMessageDTO
    {
        public int UserId { get; set; }

        public int MessageGcId { get; set; }

        public string MessageText { get; set; } = null!;
        
        public DateTime CreateAt { get; set; }
    }
}
