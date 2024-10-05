using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.Core.Services.WithHub;
using PV221Chat.DTO;
using PV221Chat.Services.Interfaces;
using PV221Chat.SignalR;

namespace PV221Chat.Services
{
    public class MessageExtension : IMessageExtension
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _notificationService;
        private readonly IMessageService _messageService;
        private readonly IGlobalChatMessageRepository _globalChatMessageRepository;
        private readonly IHubContext<GlobalChatHub> _globalChatHubContext;

        public MessageExtension(IMessageRepository messageRepository,
                              INotificationRepository notificationRepository,
                              INotificationService notificationService,
                              IMessageService messageService,
                              IUserRepository userRepository, 
                              IGlobalChatMessageRepository globalChatMessageRepository,
                              IHubContext<GlobalChatHub> globalChatHubContext)
        {
            _messageRepository = messageRepository;
            _notificationRepository = notificationRepository;
            _notificationService = notificationService;
            _messageService = messageService;
            _userRepository = userRepository;
            _globalChatMessageRepository = globalChatMessageRepository;
            _globalChatHubContext = globalChatHubContext;
        }


        public async Task<MessageDTO> SendMessageToGlobalChatAsync(string messageText, int senderId)
        {
            var globalChatMessage = new GlobalChatMessage
            {
                UserId = senderId,
                MessageText = messageText,
                CreateAt = DateTime.UtcNow
            };

            await _globalChatMessageRepository.AddDataAsync(globalChatMessage);

            var message = new MessageDTO()
            {

            };


            await _globalChatHubContext.Clients.Group("GlobalChat")
                 .SendAsync("ReceiveMessage", message);

        }

        public async Task<MessageDTO> SendMessageAsync(int chatId, string messageText, int senderId)
        {
            var message = new Message
            {
                ChatId = chatId,
                SenderId = senderId,
                MessageText = messageText,
                SentAt = DateTime.Now
            };
            message = await _messageRepository.AddDataReturnedMessageAsync(message);

            await CreateNotificationAsync(chatId, messageText, senderId);

            var unreadNotifications = await _notificationRepository.GetUnreadNotificationsByUserAndChatIdAsync(chatId, senderId);

            await _notificationService.SendNewNotificationAsync(chatId, messageText);

            var messageDTO = new MessageDTO
            {
                MessageId = message.MessageId,
                ChatId = message.ChatId,
                SenderId = message.SenderId,
                SenderName = (await _userRepository.GetDataAsync((int)message.SenderId)).Nickname,
                MessageText = message.MessageText,
                SentAt = message.SentAt,
                IsDeleted = message.IsDeleted
            };

            await _messageService.SendNewMessageAsync(chatId, messageDTO);

            return messageDTO;
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
