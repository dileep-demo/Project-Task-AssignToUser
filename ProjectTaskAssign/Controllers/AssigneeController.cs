using Microsoft.AspNetCore.Mvc;

namespace ProjectTaskAssign.Controllers
{
    public class AssigneeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
