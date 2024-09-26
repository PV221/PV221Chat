namespace PV221Chat.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Hobbies { get; set; }
        public string? Skills { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

}
