using Microsoft.AspNetCore.Mvc;

namespace ProjectTaskAssign.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
