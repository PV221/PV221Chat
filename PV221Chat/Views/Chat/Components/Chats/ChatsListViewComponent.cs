using Microsoft.AspNetCore.Mvc;
namespace PV221Chat.Views.Chat.Components.Chats
{
    public class ChatsListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var chats = new List<string> { "Общий чат", "Чат группы", "Чат событий" };
            return View(chats);
        }
    }


}
