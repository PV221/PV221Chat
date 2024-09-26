using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.DTO;
using System.Security.Claims;

namespace PV221Chat.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
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
                return View(loginDTO);
            }

            User user = await _userRepository.FindByEmail(loginDTO.Email);

            if (user != null && VerifyPasswordHash(loginDTO.Password, user.PasswordHash))
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
                return RedirectToAction("Index", "Home");
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

            var userExists = await _userRepository.FindByEmail(userDTO.Email);
            if (userExists != null)
            {
                ModelState.AddModelError(string.Empty, "User already exists.");
                return View(userDTO);
            }

            var user = new User
            {
                Nickname = userDTO.Nickname,
                Email = userDTO.Email,
                PasswordHash = CreatePasswordHash(userDTO.Password), // створити хеш пароля
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddDataAsync(user);

            return RedirectToAction("Login");
        }

        private string CreatePasswordHash(string password)
        {
            // Метод для створення хешу пароля
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var hashed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashed);
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return storedHash == Convert.ToBase64String(computedHash);
            }
        }
    }
}
