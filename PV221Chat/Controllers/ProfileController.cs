using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.DTO;
using PV221Chat.Mapper;
using System.Security.Claims;

namespace PV221Chat.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IUserChatRepository _userChatRepository;

        public ProfileController(IUserRepository userRepository, IChatRepository chatRepository, IUserChatRepository userChatRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _userChatRepository = userChatRepository;
        }

        public async Task<IActionResult> ProfileEdit()
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

            UserDTO userDTO = UserMapper.ToDTO(userExists);
            return View(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }
            
            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (userEmailClaim == null)
            {
                ModelState.AddModelError(string.Empty, "User is not authenticated.");
                return View(userDTO);
            }

            string currentUserEmail = userEmailClaim.Value;


            var userExists = await _userRepository.FindByEmailAsync(currentUserEmail);
            if (userExists == null)
            {
                ModelState.AddModelError(string.Empty, "User doesn`t exists.");
                return View(userDTO);
            }

            UserMapper.UpdateModel(userDTO, userExists);

            await _userRepository.UpdateDataAsync(userExists.UserId, userExists);

            return RedirectToAction("Profile", "Profile");
        }

        public async Task<IActionResult> Profiles()
        {
            var allUsers = await _userRepository.GetListDataAsync();
            var userDTOs = allUsers.Select(user => UserMapper.ToDTO(user)).ToList();

            return View(userDTOs);
        }

        public async Task<IActionResult> Profile(int? userId)
        {
            if (userId == null) 
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

                UserDTO userDTO = UserMapper.ToDTO(userExists);
                return View(userDTO);
            }
            var userExists1 = await _userRepository.GetDataAsync((int)userId);
            if (userExists1 == null)
            {
                return NotFound($"User with id {userId} not found.");
            }

            UserDTO userDTO2 = UserMapper.ToDTO(userExists1);
            return View(userDTO2);
        }

        public async Task<IActionResult> AddToPrivatChat(int userIdToAdd)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;

            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null || userIdToAdd == 0)
            {
                ViewBag.Message = "User not found.";
                return NotFound(); 
            }

            var chat = await _userRepository.GetPrivateChatBetweenUsersAsync(user.UserId, userIdToAdd);
            if (chat != null)
            {
                ViewBag.Message = "You already have a chat with this user.";
                return RedirectToAction("Index", "Chat");
            }

            chat = await _chatRepository.CreatePrivateChatAsync(user.UserId, userIdToAdd);
            if (chat == null)
            {
                ViewBag.Message = "An error occurred while creating the chat.";
                return BadRequest();
            }

            return RedirectToAction("Index", "Chat");
        }
    }
}
