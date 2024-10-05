using PV221Chat.Core.Services.WithHub;
using PV221Chat.Services.Interfaces;

namespace PV221Chat.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddTransient<IMessageExtension, MessageExtension>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IMessageService, MessageService>();
        }
    }
}
