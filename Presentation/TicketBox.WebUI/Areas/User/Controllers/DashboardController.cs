using Microsoft.AspNetCore.Mvc;

namespace TicketBox.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/Dashboard")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
