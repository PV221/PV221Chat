using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PV221Chat.Core.DataModels;

namespace PV221Chat.Core.DataContext;

public partial class Pv221chatContext : DbContext
{
    public Pv221chatContext()
    {
    }

    public Pv221chatContext(DbContextOptions<Pv221chatContext> options)
        : base(options)
    {
        EnsureDatabaseCreated();
    }

    public virtual DbSet<BlockedUser> BlockedUsers { get; set; }

    public virtual DbSet<BlogPage> BlogPages { get; set; }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<BlogSubscription> BlogSubscriptions { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<GlobalChatMessage> GlobalChatMessages { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<ModerationLog> ModerationLogs { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserChat> UserChats { get; set; }

    public virtual DbSet<UserRating> UserRatings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlockedUser>(entity =>
        {
            entity.HasKey(e => e.BlockId).HasName("PK__BlockedU__144215F14E325896");

            entity.HasIndex(e => e.BlockedByUserId, "IX_BlockedUsers_BlockedByUserId");

            entity.HasIndex(e => e.BlockedUserId, "IX_BlockedUsers_BlockedUserId");

            entity.HasIndex(e => e.ChatId, "IX_BlockedUsers_ChatId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.BlockedByUser).WithMany(p => p.BlockedUserBlockedByUsers)
                .HasForeignKey(d => d.BlockedByUserId)
                .HasConstraintName("FK__BlockedUs__Block__4F7CD00D");

            entity.HasOne(d => d.BlockedUserNavigation).WithMany(p => p.BlockedUserBlockedUserNavigations)
                .HasForeignKey(d => d.BlockedUserId)
                .HasConstraintName("FK__BlockedUs__Block__5070F446");

            entity.HasOne(d => d.Chat).WithMany(p => p.BlockedUsers)
                .HasForeignKey(d => d.ChatId)
                .HasConstraintName("FK__BlockedUs__ChatI__5165187F");
        });

        modelBuilder.Entity<BlogPage>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__BlogPage__54379E30072AD400");

            entity.HasIndex(e => e.AuthorId, "IX_BlogPages_AuthorId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);

            entity.HasOne(d => d.Author).WithMany(p => p.BlogPages)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__BlogPages__Autho__36B12243");
        });

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.ToTable("BlogPost");

            entity.Property(e => e.BlogPostId).ValueGeneratedNever();
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.BlogPostNavigation).WithOne(p => p.BlogPost)
                .HasForeignKey<BlogPost>(d => d.BlogPostId)
                .HasConstraintName("FK_BlogPost_BlogPages");
        });

        modelBuilder.Entity<BlogSubscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__BlogSubs__9A2B249D02618BE4");

            entity.HasIndex(e => e.BlogId, "IX_BlogSubscriptions_BlogId");

            entity.HasIndex(e => e.UserId, "IX_BlogSubscriptions_UserId");

            entity.Property(e => e.SubscribedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogSubscriptions)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK__BlogSubsc__BlogI__3A81B327");

            entity.HasOne(d => d.User).WithMany(p => p.BlogSubscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__BlogSubsc__UserI__3B75D760");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chats__A9FBE7C62A0ED89F");

            entity.Property(e => e.ChatName).HasMaxLength(100);
            entity.Property(e => e.ChatType).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<GlobalChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageGcId);

            entity.ToTable("GlobalChatMessage");

            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.MessageText).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.GlobalChatMessages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_GlobalChatMessage_Users");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9CB22DEBA9");

            entity.HasIndex(e => e.ChatId, "IX_Messages_ChatId");

            entity.HasIndex(e => e.SenderId, "IX_Messages_SenderId");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .HasConstraintName("FK__Messages__ChatId__30F848ED");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__Messages__Sender__31EC6D26");
        });

        modelBuilder.Entity<ModerationLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Moderati__5E54864890767024");

            entity.HasIndex(e => e.ModeratorId, "IX_ModerationLogs_ModeratorId");

            entity.HasIndex(e => e.TargetChatId, "IX_ModerationLogs_TargetChatId");

            entity.HasIndex(e => e.TargetUserId, "IX_ModerationLogs_TargetUserId");

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.ActionTakenAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Moderator).WithMany(p => p.ModerationLogModerators)
                .HasForeignKey(d => d.ModeratorId)
                .HasConstraintName("FK__Moderatio__Moder__49C3F6B7");

            entity.HasOne(d => d.TargetChat).WithMany(p => p.ModerationLogs)
                .HasForeignKey(d => d.TargetChatId)
                .HasConstraintName("FK__Moderatio__Targe__4BAC3F29");

            entity.HasOne(d => d.TargetUser).WithMany(p => p.ModerationLogTargetUsers)
                .HasForeignKey(d => d.TargetUserId)
                .HasConstraintName("FK__Moderatio__Targe__4AB81AF0");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E1213E3E3F1");

            entity.HasIndex(e => e.UserChatId, "IX_Notifications_UserChatId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.NotificationText).HasMaxLength(255);

            entity.HasOne(d => d.UserChat).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserChatId)
                .HasConstraintName("FK__Notificat__UserChatI__44FF419A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CA4BD31B9");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534B05749A4").IsUnique();

            entity.Property(e => e.AvatarUrl).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Hobbies).HasMaxLength(255);
            entity.Property(e => e.LoginProvider).HasMaxLength(50);
            entity.Property(e => e.Nickname).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.ProviderUserId).HasMaxLength(256);
            entity.Property(e => e.Skills).HasMaxLength(255);
        });

        modelBuilder.Entity<UserChat>(entity =>
        {
            entity.HasKey(e => e.UserChatId).HasName("PK__UserChat__B5C6DD2215EF0527");

            entity.HasIndex(e => e.ChatId, "IX_UserChats_ChatId");

            entity.HasIndex(e => e.UserId, "IX_UserChats_UserId");

            entity.Property(e => e.IsAdmin).HasDefaultValue(false);
            entity.Property(e => e.MembershipStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Chat).WithMany(p => p.UserChats)
                .HasForeignKey(d => d.ChatId)
                .HasConstraintName("FK__UserChats__ChatI__2C3393D0");

            entity.HasOne(d => d.User).WithMany(p => p.UserChats)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserChats__UserI__2B3F6F97");
        });

        modelBuilder.Entity<UserRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__UserRati__FCCDF87C35F95E37");

            entity.HasIndex(e => e.RatedById, "IX_UserRatings_RatedById");

            entity.HasIndex(e => e.RatedUserId, "IX_UserRatings_RatedUserId");

            entity.Property(e => e.RatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RatingComment).HasMaxLength(255);

            entity.HasOne(d => d.RatedBy).WithMany(p => p.UserRatingRatedBies)
                .HasForeignKey(d => d.RatedById)
                .HasConstraintName("FK__UserRatin__Rated__403A8C7D");

            entity.HasOne(d => d.RatedUser).WithMany(p => p.UserRatingRatedUsers)
                .HasForeignKey(d => d.RatedUserId)
                .HasConstraintName("FK__UserRatin__Rated__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    private void EnsureDatabaseCreated()
    {
        Database.EnsureCreated();
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
