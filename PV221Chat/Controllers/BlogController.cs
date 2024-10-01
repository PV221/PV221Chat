using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.DTO;
using PV221Chat.Mapper;
using System.Security.Claims;

namespace PV221Chat.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogPageRepository _blogPageRepository;
        private readonly IBlogSubscriptionRepository _blogSubscriptionRepository;
        private readonly IUserRepository _userRepository;

        public BlogController(IBlogPageRepository blogPageRepository, IBlogSubscriptionRepository blogSubscriptionRepository, IUserRepository userRepository)
        {
            _blogPageRepository = blogPageRepository;
            _blogSubscriptionRepository = blogSubscriptionRepository;
            _userRepository= userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            if (userEmailClaim == null)
            {
                ModelState.AddModelError(string.Empty, "User is not authenticated.");
                return RedirectToAction("Login", "Login");
            }

            string currentUserEmail = userEmailClaim.Value;


            var userExists = await _userRepository.FindByEmailAsync(currentUserEmail);

            if (userExists == null)
            {
                ModelState.AddModelError(string.Empty, "User doesn`t exists.");
                return RedirectToAction("Login", "Login");
            }
            var Pages= await _blogPageRepository.GetListDataAsync();
            if (Pages != null)
            {
                foreach (var page in Pages)
                {
                    if (page.Author == userExists) {
                        BlogPageDTO pageDTO = BlogMapper.ToDTO(page);

                        return View(pageDTO);
                    }
                }

            }
            BlogPage page1 = new BlogPage() { 
                Author = userExists,
                Title = userExists.Nickname,
                Content="",
                Type= "Personal",
                CreatedAt= DateTime.Now,
            };

            await _blogPageRepository.AddDataAsync(page1);

            BlogPageDTO pageDTO1 = BlogMapper.ToDTO(page1);

            return View(pageDTO1);
        }
    }
}
