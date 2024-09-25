using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class BlogSubscription
{
    public int SubscriptionId { get; set; }

    public int? BlogId { get; set; }

    public int? UserId { get; set; }

    public DateTime? SubscribedAt { get; set; }

    public virtual BlogPage? Blog { get; set; }

    public virtual User? User { get; set; }
}
