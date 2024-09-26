using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class UserRating
{
    public int RatingId { get; set; }

    public int? RatedUserId { get; set; }

    public int? RatedById { get; set; }

    public int? RatingScore { get; set; }

    public string? RatingComment { get; set; }

    public DateTime? RatedAt { get; set; }

    public virtual User? RatedBy { get; set; }

    public virtual User? RatedUser { get; set; }
}
