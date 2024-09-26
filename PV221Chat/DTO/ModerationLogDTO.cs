namespace PV221Chat.DTO
{
    public class ModerationLogDTO
    {
        public int LogId { get; set; }
        public int? ModeratorId { get; set; }
        public int? TargetUserId { get; set; }
        public int? TargetChatId { get; set; }
        public string Action { get; set; }
        public DateTime? ActionTakenAt { get; set; }
    }
}
