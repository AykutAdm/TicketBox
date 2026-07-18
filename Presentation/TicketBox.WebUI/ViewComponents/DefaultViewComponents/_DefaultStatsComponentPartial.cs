using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultStatsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
