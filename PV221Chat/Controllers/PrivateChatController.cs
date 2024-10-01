using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.DTO;
using System.Security.Claims;

namespace PV221Chat.Controllers
{
    public class PrivateChatController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IUserChatRepository _userChatRepository;
        public PrivateChatController(IUserRepository userRepository, IChatRepository chatRepository, IUserChatRepository userChatRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _userChatRepository = userChatRepository;
        }

        [HttpPost] 
        public async Task<IActionResult> AddToPrivatChat(int userIdToAdd)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;

            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null || userIdToAdd == 0)
            {
                ViewBag.Message = "User not found.";
                return View(); 
            }

            var chat = await _userRepository.GetPrivateChatBetweenUsersAsync(user.UserId, userIdToAdd);
            if (chat != null)
            {
                ViewBag.Message = "You already have a chat with this user.";
                return View(); 
            }

            chat = await _chatRepository.CreatePrivateChatAsync(user.UserId, userIdToAdd);
            if (chat == null)
            {
                ViewBag.Message = "An error occurred while creating the chat.";
                return View(); 
            }

            UserChat userChat1 = new UserChat
            {
                UserId = user.UserId,
                ChatId = chat.ChatId,
                IsAdmin = true
            };

            UserChat userChat2 = new UserChat
            {
                UserId = userIdToAdd,
                ChatId = chat.ChatId,
                IsAdmin = true
            };

            await _userChatRepository.AddDataAsync(userChat1);
            await _userChatRepository.AddDataAsync(userChat2);

            return RedirectToAction("Index", "Chat");
        }
    }
}
