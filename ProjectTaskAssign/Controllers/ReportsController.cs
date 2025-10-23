using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTaskAssign.Data;
using ProjectTaskAssign.ViewModels;

namespace ProjectTaskAssign.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext context;

        public ReportController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult ProjectPerformance()
        {
            var reportData = context.Project
                .Include(p => p.Tasks)
                .Select(project => new ProjectPerformanceViewModel
                {
                    ProjectName = project.Name,
                    TotalTasks = project.Tasks.Count,
                    CompletedTasks = project.Tasks.Count(t => t.status == "Complete"),
                    CompletionRate = project.Tasks.Any()
                        ? (project.Tasks.Count(t => t.status == "Complete") * 100 / project.Tasks.Count)
                        : 0
                })
                .ToList();

            return View(reportData);
        }
    }
}
