using Microsoft.AspNetCore.Mvc;

namespace PV221Chat.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
