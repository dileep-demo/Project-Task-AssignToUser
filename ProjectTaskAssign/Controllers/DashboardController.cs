using Microsoft.AspNetCore.Mvc;
using ProjectTaskAssign.Data;

namespace ProjectTaskAssign.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext context;

        public DashboardController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var totalProjects = context.Project.Count();
            var totalTasks = context.Tasks.Count();
            var totalAssignees = context.Assignees.Count();

            ViewBag.TotalProjects = totalProjects;
            ViewBag.TotalTasks = totalTasks;
            ViewBag.TotalAssignees = totalAssignees;

            return View();
        }
    }
}
