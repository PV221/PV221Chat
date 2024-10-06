using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.DTO;
using PV221Chat.Mapper;
using PV221Chat.Models;
using PV221Chat.Services;
using PV221Chat.Services.Interfaces;
using PV221Chat.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace PV221Chat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatRepository _chatRepository;
        private readonly IHubContext<GlobalChatHub> _hubContext;
        private readonly IUserRepository _userRepository;
        private readonly IMessageExtension _messageExtension;
        private readonly IGlobalChatMessageRepository _globalChatMessageRepository;

        public HomeController(IGlobalChatMessageRepository globalChatMessageRepository,IUserRepository userRepository, ILogger<HomeController> logger, IChatRepository chatRepository, IHubContext<GlobalChatHub> hubContext,IMessageExtension messageExtension)
        {
            _logger = logger;
            _chatRepository = chatRepository;
            _hubContext = hubContext;
            _userRepository = userRepository;
            _messageExtension = messageExtension;
            _globalChatMessageRepository = globalChatMessageRepository;
        }
        //[HttpGet]
        //public IActionResult Chat()
        //{
        //    var messages = new List<GlobalChatMessageDTO>();
        //    return View(messages);
        //}

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Chat()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userRepository.FindByEmailAsync(email);

            if (user == null)
            {
                RedirectToAction("Logout", "Login");
            }
            
            var list= await _globalChatMessageRepository.GetListDataAsync();
            List<GlobalChatMessageDTO> listDTO = new List<GlobalChatMessageDTO>(); ;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var dto = GlobalChatMessageMapper.ToDTO(item, (await _userRepository.GetDataAsync(item.UserId)).Nickname);
                    listDTO.Add(dto);

                }
            }
            return View(listDTO);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {

            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            User userExists = await _userRepository.FindByEmailAsync(userEmailClaim.Value);
            if (userExists == null)
            {
                    RedirectToAction("Logout", "Login");
            }

            var messageDTO = await _messageExtension.SendMessageToGlobalChatAsync(message, userExists.UserId);

            return Ok(messageDTO);
        }

        //[HttpGet("Chat/{id:int}")]
        //public async Task<IActionResult> Chat(int id)
        //{
        //    var chat = await _chatRepository.GetDataAsync(id);

        //    if (chat == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(chat);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
