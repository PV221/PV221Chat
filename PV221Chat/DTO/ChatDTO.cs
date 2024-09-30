namespace PV221Chat.DTO
{
    public class ChatDTO
    {
        public int ChatId { get; set; }
        public string? ChatName { get; set; }
        public string ChatType { get; set; }
        public bool IsOpen { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool HasUnreadMessages { get; set; }
        public string? TextUnreadMessages { get; set; }
        public int CountUnreadMessages { get; set; }
    }

}
