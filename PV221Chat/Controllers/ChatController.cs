using Microsoft.AspNetCore.Mvc;

namespace PV221Chat.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
