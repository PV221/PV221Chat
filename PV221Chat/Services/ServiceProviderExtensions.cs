using PV221Chat.Core.Services.WithHub;
using PV221Chat.DAL.Interfaces;
using PV221Chat.DAL.Repositories;
using PV221Chat.Services.Interfaces;

namespace PV221Chat.Services
{
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
            services.AddTransient<IGlobalChatMessageRepository, GlobalChatMessageRepository>();

            services.AddTransient<IDataRepository<BlockedUserRepository>, DataRepository<BlockedUserRepository>>();
            services.AddTransient<IDataRepository<BlogPageRepository>, DataRepository<BlogPageRepository>>();
            services.AddTransient<IDataRepository<BlogSubscriptionRepository>, DataRepository<BlogSubscriptionRepository>>();
            services.AddTransient<IDataRepository<ChatRepository>, DataRepository<ChatRepository>>();
            services.AddTransient<IDataRepository<MessageRepository>, DataRepository<MessageRepository>>();
            services.AddTransient<IDataRepository<ModerationLogRepository>, DataRepository<ModerationLogRepository>>();
            services.AddTransient<IDataRepository<NotificationRepository>, DataRepository<NotificationRepository>>();
            services.AddTransient<IDataRepository<UserChatRepository>, DataRepository<UserChatRepository>>();
            services.AddTransient<IDataRepository<UserRatingRepository>, DataRepository<UserRatingRepository>>();
            services.AddTransient<IDataRepository<UserRepository>, DataRepository<UserRepository>>();
            services.AddTransient<IDataRepository<GlobalChatMessageRepository>, DataRepository<GlobalChatMessageRepository>>();
        }
        public static void AddAnotherService(this IServiceCollection services)
        {
            services.AddTransient<IMessageExtension, MessageExtension>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IMessageService, MessageService>();
        }
    }
}
