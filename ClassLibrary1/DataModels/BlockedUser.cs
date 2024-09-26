using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class BlockedUser
{
    public int BlockId { get; set; }

    public int? BlockedByUserId { get; set; }

    public int? BlockedUserId { get; set; }

    public int? ChatId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? BlockedByUser { get; set; }

    public virtual User? BlockedUserNavigation { get; set; }

    public virtual Chat? Chat { get; set; }
}
