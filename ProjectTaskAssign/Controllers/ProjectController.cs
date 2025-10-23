using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTaskAssign.Data;
using ProjectTaskAssign.Models;
using ProjectTaskAssign.ViewModels;

namespace ProjectTaskAssign.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public ProjectController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

       
        public IActionResult Index()
        {
            var projects = context.Project
                .Include(p => p.Tasks)
                .ToList();

            if (!projects.Any())
            {
                ViewBag.Message = "No projects available.";
                return View(new List<ProjectViewModel>());
            }

            
            var projectViewModels = projects.Select(project => new ProjectViewModel
            {
                ProjectId = project.Id,
                Name = project.Name,
                StartDate = project.StartDate,
                TotalTasks = project.Tasks?.Count ?? 0 
            }).ToList();

            return View(projectViewModels);
        }

        
        [HttpGet]
        public IActionResult CreateProject()
        {
            return View(new ProjectViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProject(ProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please correct the errors in the form.";
                return View(projectViewModel);
            }
            var projectModel = mapper.Map<ProjectModel>(projectViewModel);
            context.Add(projectModel);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
       
        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var existingProject = context.Project
                .Include(p => p.Tasks)
                .FirstOrDefault(p => p.Id == id);

            if (existingProject == null)
            {
                return NotFound();
            }        
            var projectViewModel = mapper.Map<ProjectViewModel>(existingProject);

            return View(projectViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProject(int id, ProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please correct the errors in the form.";
                return View(projectViewModel);
            }

            // Retrieve the existing project
            var existingProject = context.Project.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if (existingProject == null)
            {
                return NotFound();
            }

            
            var updatedProject = mapper.Map<ProjectModel>(projectViewModel);
            updatedProject.Id = id; // Id is not changed

            context.Entry(updatedProject).State = EntityState.Modified;
             context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProject(int id)
        {
            var project = context.Project.Find(id);
            if (project == null)
            {
                return NotFound(); 
            }

            context.Project.Remove(project);
            context.SaveChanges();

            return RedirectToAction("Index"); 
        }
        
        [HttpGet]
        public IActionResult ViewDetails(int projectId)
        {
            // Retrieve the project and its associated tasks
            var project = context.Project
                .Include(p => p.Tasks)
                .FirstOrDefault(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            // Map the project and its tasks to a view model
            var projectDetailsViewModel = new ProjectDetailsViewModel
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                StartDate = project.StartDate,
                Tasks = project.Tasks.Select(task => new TaskViewModel
                {
                    TaskId = task.Id,
                    Name = task.Name,
                    Status = task.status,
                    CompletionDate = task.CompletionDate
                }).ToList()
            };

            return View(projectDetailsViewModel);
        }
    }
}
