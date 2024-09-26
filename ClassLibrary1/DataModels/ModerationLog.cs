using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class ModerationLog
{
    public int LogId { get; set; }

    public int? ModeratorId { get; set; }

    public string Action { get; set; } = null!;

    public int? TargetUserId { get; set; }

    public int? TargetChatId { get; set; }

    public DateTime? ActionTakenAt { get; set; }

    public virtual User? Moderator { get; set; }

    public virtual Chat? TargetChat { get; set; }

    public virtual User? TargetUser { get; set; }
}
