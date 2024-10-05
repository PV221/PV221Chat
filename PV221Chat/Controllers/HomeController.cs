using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.DTO;
using PV221Chat.Models;
using PV221Chat.SignalR;
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

        public HomeController(IUserRepository userRepository, ILogger<HomeController> logger, IChatRepository chatRepository, IHubContext<GlobalChatHub> hubContext)
        {
            _logger = logger;
            _chatRepository = chatRepository;
            _hubContext = hubContext;
            _userRepository = userRepository;
        }

        public IActionResult Chat()
        {
            var messages = new List<MessageDTO>(); 
            return View(messages);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {

            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            User userExists = await _userRepository.FindByEmailAsync(userEmailClaim.ToString());
            var messageDto = new
            {
                MessageId = Guid.NewGuid(),
                SenderId = userExists.Email,  
                MessageText = message,
                SentAt = DateTime.Now
            };

            await _hubContext.Clients.Group("GlobalChat").SendAsync("ReceiveMessage", messageDto);

            return Json(messageDto);
        }

        [HttpGet("Chat/{id:int}")]
        public async Task<IActionResult> Chat(int id)
        {
            var chat = await _chatRepository.GetDataAsync(id); 

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
    }
}
