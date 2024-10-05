using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class BlogPost
{
    public int BlogPostId { get; set; }

    public int BlogPageId { get; set; }

    public string? Title { get; set; }

    public string Text { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public virtual BlogPage BlogPostNavigation { get; set; } = null!;
}
