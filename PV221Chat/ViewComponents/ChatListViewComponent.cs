using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.DTO;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace PV221Chat.ViewComponents
{
    public class ChatListViewComponent : ViewComponent
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        public ChatListViewComponent(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
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

            // Zamień IEnumerable<Chat> na List<ChatDTO>
            return temp(chats).ToList();
        }

        private IEnumerable<ChatDTO> temp(IEnumerable<Chat> chats)
        {
            return chats.Select(chat => 
                new ChatDTO {
                    ChatId = chat.ChatId,
                    ChatName= chat.ChatName,
                    ChatType= chat.ChatType,
                    IsOpen= chat.IsOpen,
                    CreatedAt= chat.CreatedAt
                });
        }

    }
}
