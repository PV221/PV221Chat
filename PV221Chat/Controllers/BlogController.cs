using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.Interfaces;

namespace PV221Chat.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogPageRepository _blogPageRepository;
        private readonly IBlogSubscriptionRepository _blogSubscriptionRepository;

        public BlogController(IBlogPageRepository blogPageRepository, IBlogSubscriptionRepository blogSubscriptionRepository)
        {
            _blogPageRepository = blogPageRepository;
            _blogSubscriptionRepository = blogSubscriptionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
