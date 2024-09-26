using Microsoft.Extensions.DependencyInjection;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.Core.Repositories.EF;

namespace PV221Chat.Core.Services;

public static class ServiceProviderExtensions
{
    public static void AddRepositoryService(this IServiceCollection services)
    {
        services.AddTransient<IBlockedUserRepository, BlockedUserRepository>();
        services.AddTransient<IBlogPageRepository, BlogPageRepository>();
        services.AddTransient<IBlogSubscriptionRepository, BlogSubscriptionRepository>();
        services.AddTransient<IChatRepository, ChatRepository>();
        services.AddTransient<IMessageRepository, MessageRepository>();
        services.AddTransient<IModerationLogRepository, ModerationLogRepository>();
        services.AddTransient<INotificationRepository, NotificationRepository>();
        services.AddTransient<IUserChatRepository, UserChatRepository>();
        services.AddTransient<IUserRatingRepository, UserRatingRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<IDataRepository<BlockedUserRepository>, EFRepository<BlockedUserRepository>>();
        services.AddTransient<IDataRepository<BlogPageRepository>, EFRepository<BlogPageRepository>>();
        services.AddTransient<IDataRepository<BlogSubscriptionRepository>, EFRepository<BlogSubscriptionRepository>>();
        services.AddTransient<IDataRepository<ChatRepository>, EFRepository<ChatRepository>>();
        services.AddTransient<IDataRepository<MessageRepository>, EFRepository<MessageRepository>>();
        services.AddTransient<IDataRepository<ModerationLogRepository>, EFRepository<ModerationLogRepository>>();
        services.AddTransient<IDataRepository<NotificationRepository>, EFRepository<NotificationRepository>>();
        services.AddTransient<IDataRepository<UserChatRepository>, EFRepository<UserChatRepository>>();
        services.AddTransient<IDataRepository<UserRatingRepository>, EFRepository<UserRatingRepository>>();
        services.AddTransient<IDataRepository<UserRepository>, EFRepository<UserRepository>>();
    }
}
