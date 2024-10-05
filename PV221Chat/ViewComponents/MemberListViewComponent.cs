using Microsoft.AspNetCore.Mvc;
using PV221Chat.Core.DataModels;
using PV221Chat.Core.Interfaces;
using PV221Chat.DTO;
using System.Security.Claims;

namespace PV221Chat.ViewComponents
{
    public class MemberListViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        public MemberListViewComponent(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int chatId)
        {
            var members = await GetMembersAsync(chatId);
            return View(members);
        }

        private async Task<List<MemberDTO>> GetMembersAsync(int chatId)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userRepository.FindByEmailAsync(email);

            var users = await _chatRepository.GetUsersByChatId(chatId);


            return ConvertUserToMemberDTO(users, user.UserId);
        }

        private List<MemberDTO> ConvertUserToMemberDTO(IEnumerable<User> users, int userId)
        {
            List<MemberDTO> memberDTOs = new List<MemberDTO>();

            foreach (var user in users.Where(u => u.UserId != userId))
            {
                memberDTOs.Add(new MemberDTO
                {
                    UserId = user.UserId,
                    Nickname = user.Nickname,
                    Email = user.Email,
                    AvatarUrl = user.AvatarUrl
                });
            }

            return memberDTOs;
        }
    }

}
