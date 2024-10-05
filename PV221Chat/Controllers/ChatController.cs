using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
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
        public ChatController(IChatRepository chatRepository, IMessageExtension messageExtension, IUserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _messageExtension = messageExtension;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            ViewBag.ChatId = 0;
            return View();
        }

        [HttpGet("Chat/{chatId:int}")]
        public async Task<IActionResult> Index(int chatId)
        {
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
    }
}
