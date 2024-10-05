using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class BlogPage
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public int? AuthorId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string Type { get; set; } = null!;

    public virtual User? Author { get; set; }

    public virtual BlogPost? BlogPost { get; set; }

    public virtual ICollection<BlogSubscription> BlogSubscriptions { get; set; } = new List<BlogSubscription>();
}
