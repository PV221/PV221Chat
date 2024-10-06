using System;
using System.Collections.Generic;

namespace PV221Chat.Core.DataModels;

public partial class User
{
    public int UserId { get; set; }

    public string Nickname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string? LoginProvider { get; set; }

    public string? ProviderUserId { get; set; }

    public string? Hobbies { get; set; }

    public string? Skills { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? AvatarUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BlockedUser> BlockedUserBlockedByUsers { get; set; } = new List<BlockedUser>();

    public virtual ICollection<BlockedUser> BlockedUserBlockedUserNavigations { get; set; } = new List<BlockedUser>();

    public virtual ICollection<BlogPage> BlogPages { get; set; } = new List<BlogPage>();

    public virtual ICollection<BlogSubscription> BlogSubscriptions { get; set; } = new List<BlogSubscription>();

    public virtual ICollection<GlobalChatMessage> GlobalChatMessages { get; set; } = new List<GlobalChatMessage>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<ModerationLog> ModerationLogModerators { get; set; } = new List<ModerationLog>();

    public virtual ICollection<ModerationLog> ModerationLogTargetUsers { get; set; } = new List<ModerationLog>();

    public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();

    public virtual ICollection<UserRating> UserRatingRatedBies { get; set; } = new List<UserRating>();

    public virtual ICollection<UserRating> UserRatingRatedUsers { get; set; } = new List<UserRating>();
}
