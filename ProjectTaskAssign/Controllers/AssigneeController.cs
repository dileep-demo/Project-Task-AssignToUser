using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskAssign.Data;
using ProjectTaskAssign.Models;
using ProjectTaskAssign.ViewModels;

namespace ProjectTaskAssign.Controllers
{
    public class AssigneesController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public AssigneesController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: Assignees/Index

        public IActionResult Index()
        {
            var assignees = context.Assignees.ToList();

            if (!assignees.Any())
            {
                ViewBag.Message = "No Records are found";
                return View(new List<AssigneeViewModel>()); 
            }

            var assVM = mapper.Map<List<AssigneeViewModel>>(assignees);
            return View(assVM);
        }

        // GET: Assignees/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View(new AssigneeViewModel());
        }

        // POST: Assignees/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AssigneeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var assignee = new AssigneeModel
            {
                FullName = model.FullName,
                Email = model.Email
            };

            context.Assignees.Add(assignee);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Assignees/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var assignee = context.Assignees.FirstOrDefault(a => a.Id == id);
            if (assignee == null)
            {
                return NotFound();
            }

            var model = new AssigneeViewModel
            {
                AssigneeId = assignee.Id,
                FullName = assignee.FullName,
                Email = assignee.Email
            };

            return View(model);
        }

        // POST: Assignees/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AssigneeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var assignee = context.Assignees.FirstOrDefault(a => a.Id == id);
            if (assignee == null)
            {
                return NotFound();
            }

            assignee.FullName = model.FullName;
            assignee.Email = model.Email;

            context.Assignees.Update(assignee);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
