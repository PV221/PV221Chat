using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.Core.Repositories;
using PV221Chat.DTO;
using PV221Chat.ViewModels;
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
                Console.WriteLine("No blog pages found.");
            }
            else
            {
                Console.WriteLine($"Number of blog pages found: {pages.Count()}");
            }

            BlogPostViewModel viewModel = new BlogPostViewModel
            {
                User = new UserDTO
                {
                    UserId = userExists.UserId,
                    Nickname = userExists.Nickname,
                    Email = userExists.Email,
                    AvatarUrl = userExists.AvatarUrl,
                    Hobbies = userExists.Hobbies,
                    Skills = userExists.Skills,
                    BirthDate = userExists.BirthDate,
                    CreatedAt = userExists.CreatedAt
                },
                BlogPosts = new List<BlogPageDTO>()
            };

            foreach (var page in pages)
            {
                Console.WriteLine($"Processing page: {page.Title} by user ID: {page.AuthorId}");
                if (page.AuthorId == userExists.UserId)
                {
                    viewModel.BlogPosts.Add(new BlogPageDTO
                    {
                        AuthorId = page.AuthorId,
                        BlogId = page.BlogId,
                        Title = page.Title,
                        Content = page.Content,
                        Type = page.Type,
                        CreatedAt = page.CreatedAt
                    });
                }
            }

            Console.WriteLine($"Total blog posts for user: {viewModel.BlogPosts.Count}");

            if (!viewModel.BlogPosts.Any())
            {
                Console.WriteLine("No posts found. Creating a new post.");
                BlogPage newPage = new BlogPage
                {
                    Author = userExists,
                    Title = userExists.Nickname,
                    Content = "",
                    Type = "Personal",
                    CreatedAt = DateTime.Now,
                };

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

                Console.WriteLine($"Post not found with ID: {updateBlogPost.BlogId}");
            }
            else
            {
                Console.WriteLine("ModelState is not valid.");
            }

            return RedirectToAction("Index");
        }
    }
}
