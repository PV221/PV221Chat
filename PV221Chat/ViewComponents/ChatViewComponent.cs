using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.DTO;
using System.Security.Claims;

namespace PV221Chat.ViewComponents
{
    public class ChatViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;

        public ChatViewComponent(IUserRepository userRepository, IChatRepository chatRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int chatId)
        {
            ViewBag.ChatId = chatId;

            var messages = await _messageRepository.GetByChatIdAndSortedAsync(chatId, 100);

            return View(await ConvertMessageToMessageDTO(messages));
        }

        private async Task<List<MessageDTO>> ConvertMessageToMessageDTO(IEnumerable<Message> messages)
        {
            var result = new List<MessageDTO>();

            var claimsPrincipal = User as ClaimsPrincipal;
            var email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userRepository.FindByEmailAsync(email);

            foreach (var message in messages) 
            {

                result.Add(new MessageDTO
                {
                    MessageId = message.MessageId,
                    ChatId = message.ChatId,
                    SenderId = message.SenderId,
                    MessageType = message.SenderId.ToString() == user.UserId.ToString() ? "sent" : "received",
                    SenderName = (await _userRepository.GetDataAsync((int)message.SenderId)).Nickname,
                    MessageText = message.MessageText,
                    SentAt = message.SentAt,
                    IsDeleted = message.IsDeleted
                });
                
            }

            return result;
        }

    }
}
