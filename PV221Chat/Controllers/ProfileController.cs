﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.DTO;
using System.Security.Claims;

namespace PV221Chat.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Profile()
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
            UserDTO userDTO = new UserDTO()
            {
                Nickname = userExists.Nickname,
                Email = userExists.Email,
                AvatarUrl = userExists.AvatarUrl,
                Hobbies = userExists.Hobbies,
                Skills = userExists.Skills,
                BirthDate = userExists.BirthDate
            };
            return View(userDTO);
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
            UserDTO userDTO = new UserDTO()
            {
                Nickname = userExists.Nickname,
                Email = userExists.Email,
                AvatarUrl = userExists.AvatarUrl,
                Hobbies = userExists.Hobbies,
                Skills = userExists.Skills,
                BirthDate = userExists.BirthDate
            };
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
            userExists.Nickname = userDTO.Nickname;
            userExists.Email = userDTO.Email;
            userExists.AvatarUrl = userDTO.AvatarUrl;
            userExists.Hobbies = userDTO.Hobbies;
            userExists.Skills = userDTO.Skills;
            userExists.BirthDate = userDTO.BirthDate;

            await _userRepository.UpdateDataAsync(userExists.UserId, userExists);

            return RedirectToAction("Chat", "Home");
        }
    }
}
