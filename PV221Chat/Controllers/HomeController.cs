using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Models;
using System.Diagnostics;

namespace PV221Chat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatRepository _chatRepository;

        public HomeController(ILogger<HomeController> logger, IChatRepository chatRepository)
        {
            _logger = logger;
            _chatRepository = chatRepository;
        }

        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Chat(int id)
        {
            var chat = _chatRepository.GetChatById(id); 

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
