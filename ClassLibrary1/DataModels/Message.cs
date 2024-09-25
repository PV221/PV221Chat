using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class Message
{
    public int MessageId { get; set; }

    public int? ChatId { get; set; }

    public int? SenderId { get; set; }

    public string MessageText { get; set; } = null!;

    public DateTime? SentAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Chat? Chat { get; set; }

    public virtual User? Sender { get; set; }
}
