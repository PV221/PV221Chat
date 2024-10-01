using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.DTO;
using PV221Chat.Mapper;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            _userRepository = userRepository;
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
                ModelState.AddModelError(string.Empty, "User doesn't exist.");
                return RedirectToAction("Login", "Login");
            }

            Console.WriteLine($"User found: {userExists.Nickname} ({userExists.Email})");

            var pages = await _blogPageRepository.GetListDataAsync();

            if (pages == null || !pages.Any())
            {
                foreach (var page in Pages)
                {
                    if (page.Author == userExists) {
                        BlogPageDTO pageDTO = BlogMapper.ToDTO(page);

                await _blogPageRepository.AddDataAsync(newPage);

                Console.WriteLine($"New post created with ID: {newPage.BlogId}");

                viewModel.BlogPosts.Add(new BlogPageDTO
                {
                    AuthorId = newPage.AuthorId,
                    BlogId = newPage.BlogId,
                    Title = newPage.Title,
                    Content = newPage.Content,
                    Type = newPage.Type,
                    CreatedAt = newPage.CreatedAt
                });
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string content)
        {
            Console.WriteLine("CreatePost called with content: " + content);

            if (string.IsNullOrWhiteSpace(content))
            {
                ModelState.AddModelError(string.Empty, "Content cannot be empty.");
                return RedirectToAction("Index");
            }

            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var currentUserEmail = userEmailClaim?.Value;

            var userExists = await _userRepository.FindByEmailAsync(currentUserEmail);
            if (userExists != null)
            {
                var newPage = new BlogPage
                {
                    AuthorId = userExists.UserId,
                    Title = "Новый пост", 
                    Content = content,
                    Type = "Личный",
                    CreatedAt = DateTime.Now,
                };

                await _blogPageRepository.AddDataAsync(newPage);
                Console.WriteLine($"New post created with ID: {newPage.BlogId}");

                return RedirectToAction("Index");
            }

            Console.WriteLine("User not found.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlogPost(UpdateBlogPageDTO updateBlogPost)
        {
            if (ModelState.IsValid)
            {
                var existingPost = await _blogPageRepository.GetByIdAsync(updateBlogPost.BlogId);
                if (existingPost != null)
                {
                    existingPost.Title = updateBlogPost.Title;
                    existingPost.Content = updateBlogPost.Content;
                    existingPost.Type = updateBlogPost.Type;
                    existingPost.CreatedAt = DateTime.Now; 

                    await _blogPageRepository.UpdateDataAsync(existingPost);
                    return RedirectToAction("Index");
                }

            BlogPageDTO pageDTO1 = BlogMapper.ToDTO(page1);

            return RedirectToAction("Index");
        }
    }
}
