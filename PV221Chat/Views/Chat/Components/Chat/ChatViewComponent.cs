using Microsoft.AspNetCore.Mvc;

namespace PV221Chat.Views.Chat.Components.Chat
{
    public class ChatViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var messages = new List<string> { "привет!", "как дела?", "все хорошо" };  
            return View(messages);  
        }
    }


}
