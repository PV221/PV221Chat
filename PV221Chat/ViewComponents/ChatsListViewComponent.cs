using Microsoft.AspNetCore.Mvc;
using PV221Chat.DTO;

namespace PV221Chat.ViewComponents
{
    public class ChatsListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var chats = await GetChatsAsync(); 
            return View(chats);
        }

        private Task<List<ChatDTO>> GetChatsAsync()
        {
            // Zastąp to prawdziwym pobraniem danych
            return Task.FromResult(new List<ChatDTO>
        {
            new ChatDTO { ChatId = 1, ChatName = "Chat 1", IsOpen = true },
            new ChatDTO { ChatId = 2, ChatName = "Chat 2", IsOpen = false }
        });
        }
    }
}
