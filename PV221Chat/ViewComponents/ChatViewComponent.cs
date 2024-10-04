using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.DTO;

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
            var messages = new List<MessageDTO>() { 
                new MessageDTO{},
                new MessageDTO{}
            };

            return View(messages);
        }
    }


}
