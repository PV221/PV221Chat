namespace PV221Chat.DTO
{
    public class GroupEditDTO
    {
        public int chatId { get; set; }
        public string GroupName { get; set; }
        public List<UserDTO> users { get; set; }
        public List<int> UsersInGroup { get; set; }
    }
}
