using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.DTO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using PV221Chat.Core.Repositories;

namespace PV221Chat.Controllers
{
    public class LoginController : Controller
    {
        private readonly IBlogPageRepository _blogPageRepository;
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository, IBlogPageRepository blogPageRepository)
        {
            _blogPageRepository = blogPageRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            User user = await _userRepository.FindByEmailAsync(loginDTO.Email);

            if (user != null && user.PasswordHash == CalculateHash(loginDTO.Password, loginDTO.Email))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Nickname),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Chat", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(loginDTO);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }

            var userExists = await _userRepository.FindByEmailAsync(userDTO.Email);
            if (userExists != null)
            {
                ModelState.AddModelError(string.Empty, "User already exists.");
                return View(userDTO);
            }

            var user = new User
            {
                Nickname = userDTO.Nickname,
                Email = userDTO.Email,
                PasswordHash = CalculateHash(userDTO.Password, userDTO.Email), // створити хеш пароля
                CreatedAt = DateTime.UtcNow
            };


            await _userRepository.AddDataAsync(user);

            BlogPage page = new BlogPage()
            {
                Author = user,
                Title = userExists.Nickname,
                Content = "",
                Type = "Personal",
                CreatedAt = DateTime.Now,
            };
            await _blogPageRepository.AddDataAsync(page);

            return RedirectToAction("Chat","Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction("Login", "Login");
        }


        private string CalculateHash(string clearTextPassword, string salt)
        {
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);

            HashAlgorithm algorithm = SHA256.Create();

            byte[] hash = algorithm.ComputeHash(saltedHashBytes);

            return Convert.ToBase64String(hash);
        }
    }
}
