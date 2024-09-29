using Microsoft.AspNetCore.Mvc;
namespace PV221Chat.Views.Chat.Components.Members
{
    public class MemberListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();  
        }
    }

}
