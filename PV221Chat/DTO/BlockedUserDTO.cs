namespace PV221Chat.DTO
{
    public class BlockedUserDTO
    {
        public int BlockId { get; set; }
        public int? BlockedByUserId { get; set; }
        public int? BlockedUserId { get; set; }
        public int? ChatId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

}
