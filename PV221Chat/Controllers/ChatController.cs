using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.DTO;
using PV221Chat.Mapper;
using PV221Chat.Models;
using PV221Chat.Services;
using PV221Chat.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace PV221Chat.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IMessageExtension _messageExtension;
        private readonly IUserChatRepository _userChatRepository;
        private readonly INotificationRepository _notificationRepository;
        public ChatController(IChatRepository chatRepository, IMessageExtension messageExtension, IUserRepository userRepository, IUserChatRepository userChatRepository, INotificationRepository notificationRepository)
        {
            _chatRepository = chatRepository;
            _messageExtension = messageExtension;
            _userRepository = userRepository;
            _userChatRepository = userChatRepository;
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ChatId = 0;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int chatId)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userRepository.FindByEmailAsync(email);

            if (user != null && chatId != 0)
            {
                var userChat = await _userChatRepository.FindUserChatByUserIdAndChatIdAsync(user.UserId, chatId);

                if(userChat != null)
                {
                    await _notificationRepository.ReadNotifiByUserChatId(userChat.UserChatId);
                }
            }


            var chat = await _chatRepository.GetDataAsync(chatId);

            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int chatId, string message)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userRepository.FindByEmailAsync(email);

            var messageDTO = await _messageExtension.SendMessageAsync(chatId, message, user.UserId);

            return Ok(messageDTO);
        }

        [HttpGet]
        public async Task<IActionResult> CreateNewGroupChat()
        {
            var users = await _userRepository.GetListDataAsync();
            var usersDTO = users.Select(user => UserMapper.ToDTO(user)).ToList();
            return View(usersDTO);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateNewGroupChat(int temp)
        //{
        //    var users = await _userRepository.GetListDataAsync();
        //    var usersDTO = users.Select(user => UserMapper.ToDTO(user));
        //    return View(usersDTO);
        //}

        [HttpPost]
        public async Task<IActionResult> AddUsersToChat(int chatId)
        {
            var users = await _userRepository.GetListDataAsync();
            var usersDTO = users.Select(user => UserMapper.ToDTO(user)).ToList();

            var groupEditDTO = new GroupEditDTO
            {
                users = usersDTO
            };

            return View(groupEditDTO);
        }
    }
}
