using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
                Status = task.status,
                CompletionDate = task.CompletionDate
            }).ToList();

            return View(taskViewModels);
        }


        //public IActionResult Index(int projectId)
        //{
        //    var tasks = context.Tasks
        //        .Include(t => t.ProjectModel)
        //        .Where(t => t.ProjectId == projectId)
        //        .ToList();

        //    if (!tasks.Any())
        //    {
        //        ViewBag.Message = "No tasks available.";
        //        return View(new List<TaskViewModel>());
        //    }

        //    var taskViewModels = tasks.Select(task => new TaskViewModel
        //    {
        //        TaskId = task.Id,
        //        Name = task.Name,
        //        ProjectName = task.ProjectModel.Name,
        //        Status = task.status,
        //        CompletionDate = task.CompletionDate
        //    }).ToList();

        //    return View(taskViewModels);
        //}

        [HttpGet]
        public IActionResult CreateTask(int projectId)
        {
            // Populate the ViewBag with the list of projects
            ViewBag.Projects = context.Project
                .Select(p => new { p.Id, p.Name })
                .ToList();

            var taskViewModel = new TaskViewModel
            {
                ProjectId = projectId
            };
            return View(taskViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTask(TaskViewModel taskViewModel)
        {
            ViewBag.Projects = context.Project
                .Select(p => new { p.Id, p.Name })
                .ToList();

            if (ModelState.IsValid)
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
                .FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
            {
                return NotFound();
            }

            var taskViewModel = mapper.Map<TaskViewModel>(existingTask);
            return View(taskViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTask(int id, TaskViewModel taskViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please correct the errors in the form.";
                return View(taskViewModel);
            }

            var existingTask = context.Tasks.Find(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            mapper.Map(taskViewModel, existingTask);
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