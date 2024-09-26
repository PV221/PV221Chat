using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class Chat
{
    public int ChatId { get; set; }

    public string? ChatName { get; set; }

    public string ChatType { get; set; } = null!;

    public bool IsOpen { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BlockedUser> BlockedUsers { get; set; } = new List<BlockedUser>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<ModerationLog> ModerationLogs { get; set; } = new List<ModerationLog>();

    public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();
}
