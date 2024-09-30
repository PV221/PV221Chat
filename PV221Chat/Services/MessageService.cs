using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Services.WithHub;
using PV221Chat.Services.Interfaces;

namespace PV221Chat.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _notificationService;

        public MessageService(IMessageRepository messageRepository,
                              INotificationRepository notificationRepository,
                              INotificationService notificationService)
        {
            _messageRepository = messageRepository;
            _notificationRepository = notificationRepository;
            _notificationService = notificationService;
        }

        public async Task SendMessageAsync(int chatId, string messageText, int senderId)
        {
            var message = new Message
            {
                ChatId = chatId,
                SenderId = senderId,
                MessageText = messageText,
                SentAt = DateTime.Now
            };
            await _messageRepository.AddDataAsync(message);

            await CreateNotificationAsync(chatId, messageText, senderId);

            var unreadNotifications = await _notificationRepository.GetUnreadNotificationsByUserAndChatIdAsync(chatId, senderId);
            int unreadCount = unreadNotifications.Count();

            await _notificationService.SendNewNotificationAsync(chatId, messageText, unreadCount);
        }

        private async Task CreateNotificationAsync(int chatId, string messageText, int senderId)
        {
            var usersInChat = await _notificationRepository.GetUsersInChatExceptSenderAsync(chatId, senderId);

            foreach (var userChat in usersInChat)
            {
                var notification = new Notification
                {
                    UserChatId = userChat.UserChatId,
                    NotificationText = messageText,
                    CreatedAt = DateTime.Now,
                    IsRead = false
                };
                await _notificationRepository.AddDataAsync(notification);
            }
        }
    }
}
