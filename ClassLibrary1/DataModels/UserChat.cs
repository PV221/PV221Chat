using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class UserChat
{
    public int UserChatId { get; set; }

    public int? UserId { get; set; }

    public int? ChatId { get; set; }

    public bool? IsAdmin { get; set; }

    public string? MembershipStatus { get; set; }

    public virtual Chat? Chat { get; set; }

    public virtual User? User { get; set; }
}
