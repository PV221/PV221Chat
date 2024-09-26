using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string? NotificationText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsRead { get; set; }

    public virtual User? User { get; set; }
}
