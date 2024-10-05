using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.DTO;
using System.Data;
using System.Security.Claims;

namespace PV221Chat.ViewComponents
{
    public class ChatListViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMessageRepository _messageRepository;

        public ChatListViewComponent(IUserRepository userRepository, IChatRepository chatRepository, INotificationRepository notificationRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _notificationRepository = notificationRepository;
            _messageRepository = messageRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int chatId)
        {
            ViewBag.ChatId = chatId;

            var chats = await GetChatsAsync(); 
            return View(chats);
        }

        private async Task<List<ChatDTO>> GetChatsAsync()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return new List<ChatDTO>();
            }

            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null)
            {
                return new List<ChatDTO>();
            }

            var chats = await _chatRepository.GetDataByUserIdAsync(user.UserId);

            return (await ConvertChatsToChatDTOs(chats, user.UserId)).ToList();
        }


        private async Task<IEnumerable<ChatDTO>> ConvertChatsToChatDTOs(IEnumerable<Chat> chats, int userId)
        {
            List<ChatDTO> chatDTOs = new List<ChatDTO>();

            foreach (var chat in chats)
            {
                var unreadNotifications = await _notificationRepository.GetUnreadNotificationsByUserAndChatIdAsync(chat.ChatId, userId);

                if (string.IsNullOrEmpty(chat.ChatName) && chat.ChatType == "Private")
                {
                    var users = await _userRepository.FindByChatIdAsync(chat.ChatId);
                    if(users.Count() == 2)
                    {
                        var user = users.FirstOrDefault(u => u.UserId != userId);
                        chat.ChatName = user.Nickname;
                    }
                }

                var text = "";

                if(unreadNotifications.Count() != 0)
                {
                    text = unreadNotifications.Last().NotificationText;
                }

                var lastMessage = await _messageRepository.FindLastMessageByChatIdAsync(chat.ChatId);
                var lastMessageAt = chat.CreatedAt;

                if (lastMessage != null)
                {
                    lastMessageAt = lastMessage.SentAt;
                }

                chatDTOs.Add(new ChatDTO
                {
                    ChatId = chat.ChatId,
                    ChatName = chat.ChatName,
                    ChatType = chat.ChatType,
                    IsOpen = chat.IsOpen, 
                    CreatedAt = chat.CreatedAt,
                    LastMessageAt = lastMessageAt,
                    HasUnreadMessages = unreadNotifications.Any(),
                    TextUnreadMessages = text,
                    CountUnreadMessages = unreadNotifications.Count()
                });
            }

            chatDTOs.OrderBy(chatDTO => chatDTO.LastMessageAt);

            return chatDTOs;
        }
    }
}
