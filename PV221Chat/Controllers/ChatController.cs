using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.Models;
using System.Diagnostics;

namespace PV221Chat.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatRepository _chatRepository;
        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Index(int id)
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
