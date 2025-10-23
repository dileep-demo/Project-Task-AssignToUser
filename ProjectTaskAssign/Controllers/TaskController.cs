using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTaskAssign.Data;
using ProjectTaskAssign.Models;
using ProjectTaskAssign.ViewModels;

namespace ProjectTaskAssign.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public TaskController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var tasks = context.Tasks
                .Include(t => t.ProjectModel) 
                .Include(t => t.Assignee)
                .ToList();

            if (!tasks.Any())
            {
                ViewBag.Message = "No tasks available.";
                return View(new List<TaskViewModel>());
            }

            var taskViewModels = tasks.Select(task => new TaskViewModel
            {
                TaskId = task.Id,
                Name = task.Name,
                ProjectName = task.ProjectModel?.Name, 
                FullName = task.Assignee.FullName,
                Status = task.status,
                CompletionDate = task.CompletionDate
            }).ToList();

            return View(taskViewModels);
        }
        [HttpGet]
        public IActionResult CreateTask(int projectId)
        {
            ViewBag.Projects = new SelectList(context.Project, "Id", "Name", projectId);

            // Convert to SelectListItem
            ViewBag.Assignees = context.Assignees
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                })
                .ToList();

            var taskViewModel = new TaskViewModel
            {
                ProjectId = projectId // Pre-select the project if projectId is provided
            };
            return View(taskViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTask(TaskViewModel taskViewModel)
        {
            ViewBag.Projects = new SelectList(context.Project, "Id", "Name", taskViewModel.ProjectId);

            // Convert to SelectListItem
            ViewBag.Assignees = context.Assignees
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                })
                .ToList();

            ModelState.Remove(nameof(taskViewModel.ProjectName));
            ModelState.Remove(nameof(taskViewModel.FullName));

            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please correct the errors in the form.";
                return View(taskViewModel);
            }

            var taskModel = mapper.Map<TaskModel>(taskViewModel);
            context.Add(taskModel);
            context.SaveChanges();

            return RedirectToAction("Index", new { projectId = taskViewModel.ProjectId });
        }
        [HttpGet]
        public IActionResult UpdateTask(int id)
        {
            var existingTask = context.Tasks
                .Include(t => t.ProjectModel)
                .Include(t => t.Assignee)
                .FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
            {
                return NotFound();
            }

            ViewBag.StatusList = new SelectList(new[]
            {
        new { Value = "Pending", Text = "Pending" },
        new { Value = "In Progress", Text = "In Progress" },
        new { Value = "Complete", Text = "Complete" }
    }, "Value", "Text", existingTask.status);

            ViewBag.Projects = new SelectList(context.Project, "Id", "Name", existingTask.ProjectId);

            // Populate the dropdown for usernames
            ViewBag.Assignees = context.Assignees
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                })
                .ToList();

            var taskViewModel = mapper.Map<TaskViewModel>(existingTask);
            return View(taskViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTask(int id, TaskViewModel taskViewModel)
        {
            ModelState.Remove(nameof(taskViewModel.ProjectName));
            ModelState.Remove(nameof(taskViewModel.FullName));

            ViewBag.StatusList = new SelectList(new[]
            {
        new { Value = "Pending", Text = "Pending" },
        new { Value = "In Progress", Text = "In Progress" },
        new { Value = "Complete", Text = "Complete" }
    }, "Value", "Text", taskViewModel.Status);

            ViewBag.Projects = new SelectList(context.Project, "Id", "Name", taskViewModel.ProjectId);

            // Populate the dropdown for usernames
            ViewBag.Assignees = context.Assignees
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                })
                .ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please correct the errors in the form.";
                return View(taskViewModel);
            }

            var existingTask = context.Tasks
                .Include(t => t.ProjectModel)
                .FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Name = taskViewModel.Name;
            existingTask.status = taskViewModel.Status;
            existingTask.CompletionDate = taskViewModel.CompletionDate;

            if (existingTask.ProjectId != taskViewModel.ProjectId)
            {
                existingTask.ProjectId = taskViewModel.ProjectId;
                existingTask.ProjectModel = null;
            }

            if (existingTask.AssigneeId != taskViewModel.AssigneeId)
            {
                existingTask.AssigneeId = taskViewModel.AssigneeId;
                existingTask.Assignee = null;
            }

            context.Update(existingTask);
            context.SaveChanges();

            return RedirectToAction("Index", new { projectId = taskViewModel.ProjectId });
        }
       
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTask(int id, int projectId)
        {
            var task = context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            context.Tasks.Remove(task);
            context.SaveChanges();

            return RedirectToAction("Index", new { projectId });
        }
    }
}
