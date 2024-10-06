using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class GlobalChatMessage
{
    public int MessageGcId { get; set; }

    public int UserId { get; set; }

    public string MessageText { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public virtual User User { get; set; } = null!;
}
